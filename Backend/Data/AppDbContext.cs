using Microsoft.EntityFrameworkCore;
using InfiniSoft.Admin.Models;

namespace InfiniSoft.Admin.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<BlogPost> BlogPosts { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
    public DbSet<SiteStatistic> SiteStatistics { get; set; } = null!;
}
