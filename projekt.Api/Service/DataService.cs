using Microsoft.EntityFrameworkCore;
using projekt.Api.Data;
using Shared;
namespace projekt.Service;

public class DataService
{
    private AppContextDb db { get; }

    public DataService(AppContextDb db)
    {
        this.db = db;
    }

    /// <summary>
    /// Seeder 15 posts til databasen
    /// </summary>
    public void SeedData()
    {
        var newPosts = new List<Post>
        {
            new Post { Title = "Post 1", Author = "Bruger2", Content = "Indhold 3", CreatedAt = DateTime.Now },
            new Post { Title = "Post 2", Author = "Bruger3", Content = "Indhold 4", CreatedAt = DateTime.Now },
            new Post { Title = "Post 3", Author = "Bruger4", Content = "Indhold 5", CreatedAt = DateTime.Now },
            new Post { Title = "Post 4", Author = "Bruger5", Content = "Indhold 6", CreatedAt = DateTime.Now },
            new Post { Title = "Post 5", Author = "Bruger6", Content = "Indhold 7", CreatedAt = DateTime.Now },
            new Post { Title = "Post 6", Author = "Bruger7", Content = "Indhold 8", CreatedAt = DateTime.Now },
            new Post { Title = "Post 7", Author = "Bruger8", Content = "Indhold 9", CreatedAt = DateTime.Now },
            new Post { Title = "Post 8", Author = "Bruger9", Content = "Indhold 10", CreatedAt = DateTime.Now },
            new Post { Title = "Post 9", Author = "Bruger10", Content = "Indhold 11", CreatedAt = DateTime.Now },
            new Post { Title = "Post 10", Author = "Bruger11", Content = "Indhold 12", CreatedAt = DateTime.Now },
            new Post { Title = "Post 11", Author = "Bruger12", Content = "Indhold 13", CreatedAt = DateTime.Now },
            new Post { Title = "Post 12", Author = "Bruger13", Content = "Indhold 14", CreatedAt = DateTime.Now },
            new Post { Title = "Post 13", Author = "Bruger14", Content = "Indhold 15", CreatedAt = DateTime.Now },
            new Post { Title = "Post 14", Author = "Bruger15", Content = "Indhold 16", CreatedAt = DateTime.Now },
            new Post { Title = "Post 15", Author = "Bruger16", Content = "Indhold 17", CreatedAt = DateTime.Now }
        };

        db.Posts.AddRange(newPosts);
        db.SaveChanges();
    }

    // --- POST operationer ---
    public List<Post> GetPosts()
    {
        return db.Posts.Include(p => p.Comments)
                       .OrderByDescending(p => p.CreatedAt)
                       .ToList();
    }

    public Post? GetPost(int id)
    {
        return db.Posts.Include(p => p.Comments)
                       .FirstOrDefault(p => p.Id == id);
    }

    public string CreatePost(Post post)
    {
        post.CreatedAt = DateTime.Now;
        db.Posts.Add(post);
        db.SaveChanges();
        return "Post created";
    }

    public void UpvotePost(int id)
    {
        var post = db.Posts.Find(id);
        if (post != null) { post.Upvotes++; db.SaveChanges(); }
    }

    public void DownvotePost(int id)
    {
        var post = db.Posts.Find(id);
        if (post != null) { post.Downvotes++; db.SaveChanges(); }
    }

    // --- COMMENT operationer ---
    public void CreateComment(int postId, Comment comment)
    {
        comment.DatePosted = DateTime.Now;
        comment.PostId = postId;
        db.Comments.Add(comment);
        db.SaveChanges();
    }

    public void UpvoteComment(int postId, int commentId)
    {
        var comment = db.Comments.FirstOrDefault(c => c.PostId == postId && c.Id == commentId);
        if (comment != null) { comment.Upvotes++; db.SaveChanges(); }
    }

    public void DownvoteComment(int postId, int commentId)
    {
        var comment = db.Comments.FirstOrDefault(c => c.PostId == postId && c.Id == commentId);
        if (comment != null) { comment.Downvotes++; db.SaveChanges(); }
    }
}
