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
            entity.HasKey(k => k.Id).HasName("PK_User");

            entity.HasIndex(i => i.Email).IsUnique(true);

            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.Surname).IsRequired(true);
            entity.Property(p => p.Email).IsRequired(true);
            entity.Property(p => p.PasswordHash).IsRequired(true);
        });

        modelBuilder.Entity<Item>(entity => {
            entity.HasKey(k => k.Id).HasName("PK_Item");

            entity.HasIndex(i => i.Name).IsUnique(true);
            
            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.SubcategoryId).IsRequired(true);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_Category");

            entity.HasIndex(i => i.Name).IsUnique(true);

            entity.Property(p => p.Name).IsRequired(true);
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_Subcategory");

            entity.HasIndex(i => i.Name).IsUnique(true);

            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.CategoryId).IsRequired(true);

            entity.HasOne(a => a.Category)
            .WithMany(b => b.Subcategories)
            .HasForeignKey(c => c.CategoryId)
            .HasConstraintName("FK_SubcategoryToCategory");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_ShoppingList");

            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.UserId).IsRequired(true);

            entity.HasOne(a => a.User)
            .WithMany(b => b.ShoppingLists)
            .HasForeignKey(c => c.UserId)
            .HasConstraintName("FK_ShoppingListToUser");
        });

        modelBuilder.Entity<ShoppingListContent>(entity =>
        {
            entity.HasKey(a => new { a.ListId, a.ItemId }).HasName("CK_ListContent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
