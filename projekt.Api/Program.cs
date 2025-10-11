using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projekt.Api.Data;
using projekt.Service;
using Shared;


var builder = WebApplication.CreateBuilder(args);

// --- CORS setup ---
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// --- DbContext og DataService ---
builder.Services.AddDbContext<AppContextDb>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));
builder.Services.AddScoped<DataService>();

var app = builder.Build();

// --- Aktiver CORS ---
app.UseCors(AllowSomeStuff);

// --- Seed data ---
// using (var scope = app.Services.CreateScope())
// {
//     var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
//     dataService.SeedData();
// }

// --- Seed data ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppContextDb>();
    db.Database.Migrate(); // <-- sørger for at tabellerne oprettes
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData();
}

// --- API Endpoints ---
// Get all posts
app.MapGet("/api/posts", (DataService dataService) => dataService.GetPosts());

// Get single post
app.MapGet("/api/posts/{id}", (int id, DataService dataService) => dataService.GetPost(id));

// Create post
app.MapPost("/api/posts", (Post post, DataService dataService) => dataService.CreatePost(post));

// Upvote / Downvote post
app.MapPut("/api/posts/{id}/upvote", (int id, DataService dataService) => dataService.UpvotePost(id));
app.MapPut("/api/posts/{id}/downvote", (int id, DataService dataService) => dataService.DownvotePost(id));

// Create comment
app.MapPost("/api/posts/{postId}/comments", (int postId, Comment comment, DataService dataService) =>
{
    dataService.CreateComment(postId, comment);
});

// Upvote / Downvote comment
app.MapPut("/api/posts/{postId}/comments/{commentId}/upvote", (int postId, int commentId, DataService dataService) =>
    dataService.UpvoteComment(postId, commentId));
app.MapPut("/api/posts/{postId}/comments/{commentId}/downvote", (int postId, int commentId, DataService dataService) =>
    dataService.DownvoteComment(postId, commentId));


app.Run();
