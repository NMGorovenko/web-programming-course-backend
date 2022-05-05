using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sfu.Shop.Domain.Chat;
using Sfu.Shop.Domain.Entities;
using Sfu.Shop.Domain.IdentityEntities;

namespace Sfu.Shop.Infrastructure.DataAccess;

public class AppDbContext : IdentityDbContext<User, AppIdentityRole, Guid>
{
    /// <summary>
    /// Products.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Feedbacks.
    /// </summary>
    public DbSet<Feedback> Feedbacks { get; set; }
    
    /// <summary>
    /// Chat rooms.
    /// </summary>
    public DbSet<ChatRoom> ChatRooms { get; set; }
    
    /// <summary>
    /// Messages.
    /// </summary>
    public DbSet<Message> Messages { get; set; }
 
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Options.</param>
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Shop.
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Product)
            .WithMany(p => p.Feedback)
            .HasForeignKey(f => f.ProductId);
        
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.FeedbackUser)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.FeedbackUserId);
        
        // Chat.
        modelBuilder.Entity<ChatRoom>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.ChatRoom)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatRoomId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.CreatedBy)
            .WithMany()
            .HasForeignKey(m => m.CreatedById);
    }
    
}
