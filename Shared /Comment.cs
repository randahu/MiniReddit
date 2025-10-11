using System.Text.Json.Serialization;
namespace Shared;

public class Comment
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public DateTime DatePosted { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public int score => Upvotes - Downvotes; 
    
    //realtioner 
    public int PostId { get; set; }
    
    [JsonIgnore] // Undgår circular reference når du henter data som JSON
    public Post? Post { get; set; }
   
}