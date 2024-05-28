namespace AspNetServer.Models;

using System.Text.Json.Serialization;

public class Book
{
    public string? Title {get; set;}
    public string? Author {get; set;}
    [JsonPropertyName("cover_image")]
    public string? Cover_Image {get; set;}
    public bool Borrowed {get; set;}
}