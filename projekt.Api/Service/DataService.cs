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
        if (db.Posts.Any()) return;

        // 1️⃣ Opret brugere
        var users = new List<User>
        {
            new User { Username = "Bruger2" },
            new User { Username = "Bruger3" },
            new User { Username = "Bruger4" },
            new User { Username = "Bruger5" },
            new User { Username = "Bruger6" },
            new User { Username = "Bruger7" },
            new User { Username = "Bruger8" },
            new User { Username = "Bruger9" },
            new User { Username = "Bruger10" },
            new User { Username = "Bruger11" },
            new User { Username = "Bruger12" },
            new User { Username = "Bruger13" },
            new User { Username = "Bruger14" },
            new User { Username = "Bruger15" },
            new User { Username = "Bruger16" }
        };
        
        var newPosts = new List<Post>
        {
            new Post { Title = "Post 1", Content = "Indhold 3", CreatedAt = DateTime.Now, User = users[0] },
            new Post { Title = "Post 2", Content = "Indhold 4", CreatedAt = DateTime.Now, User = users[1] },
            new Post { Title = "Post 3", Content = "Indhold 5", CreatedAt = DateTime.Now, User = users[2] },
            new Post { Title = "Post 4", Content = "Indhold 6", CreatedAt = DateTime.Now, User = users[3] },
            new Post { Title = "Post 5", Content = "Indhold 7", CreatedAt = DateTime.Now, User = users[4] },
            new Post { Title = "Post 6", Content = "Indhold 8", CreatedAt = DateTime.Now, User = users[5] },
            new Post { Title = "Post 7", Content = "Indhold 9", CreatedAt = DateTime.Now, User = users[6] },
            new Post { Title = "Post 8", Content = "Indhold 10", CreatedAt = DateTime.Now, User = users[7] },
            new Post { Title = "Post 9", Content = "Indhold 11", CreatedAt = DateTime.Now, User = users[8] },
            new Post { Title = "Post 10", Content = "Indhold 12", CreatedAt = DateTime.Now, User = users[9] },
            new Post { Title = "Post 11", Content = "Indhold 13", CreatedAt = DateTime.Now, User = users[10] },
            new Post { Title = "Post 12", Content = "Indhold 14", CreatedAt = DateTime.Now, User = users[11] },
            new Post { Title = "Post 13", Content = "Indhold 15", CreatedAt = DateTime.Now, User = users[12] },
            new Post { Title = "Post 14", Content = "Indhold 16", CreatedAt = DateTime.Now, User = users[13] },
            new Post { Title = "Post 15", Content = "Indhold 17", CreatedAt = DateTime.Now, User = users[14] }
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
