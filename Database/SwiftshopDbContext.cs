using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Swiftshop.Database;

public partial class SwiftshopDbContext : DbContext
{
    public SwiftshopDbContext()
    {
    }

    public SwiftshopDbContext(DbContextOptions<SwiftshopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SwiftshopDB;Trusted_Connection=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Content");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ListId).HasColumnName("list_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany()
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_Item");

            entity.HasOne(d => d.List).WithMany()
                .HasForeignKey(d => d.ListId)
                .HasConstraintName("FK_ShoppingList");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3213E83FE4B637D1");

            entity.ToTable("Item");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shopping__3213E83FCA11AF41");

            entity.ToTable("ShoppingList");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ListContent).HasColumnName("list_content");
            entity.Property(e => e.ListUser).HasColumnName("list_user");

            entity.HasOne(d => d.ListUserNavigation).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.ListUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07506CF3B6");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Surname)
                .IsUnicode(false)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
