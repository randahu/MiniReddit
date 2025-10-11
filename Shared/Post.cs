namespace Shared;

public class Post
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public string Author { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }


    public List<Comment> Comments { get; set; } = new(); 
}
