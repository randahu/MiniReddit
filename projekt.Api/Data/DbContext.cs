using Microsoft.EntityFrameworkCore;
using Shared;

namespace projekt.Api.Data;

public class AppContextDb : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    public AppContextDb(DbContextOptions<AppContextDb> options)
        : base(options)
    {
    }
}