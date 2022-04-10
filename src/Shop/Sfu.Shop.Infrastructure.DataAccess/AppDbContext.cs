﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
    /// Categories.
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    
    /// <summary>
    /// Feedbacks.
    /// </summary>
    public DbSet<Feedback> Feedbacks { get; set; }
    
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

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Product)
            .WithMany(p => p.Feedback)
            .HasForeignKey(f => f.ProductId);
        
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.FeedbackUser)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.FeedbackUserId);
    }
    
}