using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Models;

namespace Swiftshop.Database;

public partial class SwiftshopDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Subcategory> Subcategories { get; set; }
    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
    public virtual DbSet<ShoppingListContent> ShoppingListContents { get; set; }

    public SwiftshopDbContext()
    {
    }

    public SwiftshopDbContext(DbContextOptions<SwiftshopDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SwiftshopDB;Trusted_Connection=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");
            entity.HasIndex(a => a.Email).IsUnique();
        });

        modelBuilder.Entity<Item>(entity => {
            entity.HasKey(e => e.Id).HasName("PK_Item");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Category");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Subcategory");

            entity.HasOne(a => a.Category)
            .WithMany(b => b.Subcategories)
            .HasForeignKey(c => c.categoryId)
            .HasConstraintName("FK_SubcategoryToCategory");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ShoppingList");

            entity.HasOne(a => a.User)
            .WithMany(b => b.ShoppingLists)
            .HasForeignKey(c => c.userId)
            .HasConstraintName("FK_ShoppingListToUser");

        });

        modelBuilder.Entity<ShoppingListContent>(entity =>
        {
            entity.HasKey(a => new { a.listId, a.itemId }).HasName("CK_ListContent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
