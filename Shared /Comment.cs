using System.Text.Json.Serialization;
namespace Shared;

public class Comment
{
    public int Id { get; set; }
    // public string AuthorName { get; set; }
    public string Content { get; set; }
    public DateTime DatePosted { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public int score => Upvotes - Downvotes; 
    public int? UserId { get; set; }
    public User User { get; set; }
    
    public Comment( string content = "", int upvotes = 0, int downvotes = 0, User user = null)
    {
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        User = user;
    }
    
    public Comment ()
    {
        Id = 0;
        Content = "";
        Upvotes = 0;
        Downvotes = 0;
    }
    //realtioner 
    public int PostId { get; set; }
    
    [JsonIgnore] // Undgår circular reference når du henter data som JSON
    public Post? Post { get; set; }
   
}