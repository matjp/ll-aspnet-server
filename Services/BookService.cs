using AspNetServer.Models;
using MySqlConnector;

namespace AspNetServer.Services;

public class BookService {
    
    private readonly MySqlConnection _connection;

    public BookService(MySqlConnection connection) {
        _connection = connection;
        _connection.Open();
    }

    public async Task<List<Book>> Get(int borrowed) {
        var books = new List<Book> {};
        var whereClause = borrowed == 1 ? " where borrowed = 1 " : "" ;
        using var command = new MySqlCommand($"SELECT title, author, TO_BASE64(cover_image) as cover_image, borrowed FROM book {whereClause} ORDER BY title;", _connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var borrowedVal = reader.GetInt16("borrowed");
            var book = new Book
            {
                Title = reader.GetString("title"),
                Author = reader.GetString("author"),
                Cover_Image = reader.GetString("cover_image"),
                Borrowed = borrowedVal == 1
            };
            books.Add(book);
        }
        return books;
    }

    public async Task Borrow(string title) {
        using var command = new MySqlCommand($"UPDATE book SET borrowed = 1 WHERE title = \"{title}\";", _connection);
        await command.ExecuteNonQueryAsync();
    }

    public async Task Return(string title) {
        using var command = new MySqlCommand($"UPDATE book SET borrowed = 0 WHERE title = \"{title}\";", _connection);
        await command.ExecuteNonQueryAsync();
    }    
}