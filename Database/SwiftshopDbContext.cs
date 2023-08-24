using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swiftshop.Database;

public class SwiftshopDbContext : IdentityDbContext<User>
{
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Subcategory> Subcategories { get; set; }
    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
    public virtual DbSet<ShoppingListContent> ShoppingListContents { get; set; }


    public SwiftshopDbContext(DbContextOptions<SwiftshopDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SwiftshopDB;Trusted_Connection=True;MultipleActiveResultSets=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("SubcategoryId");
        modelBuilder.HasSequence<int>("ItemId");
        modelBuilder.HasSequence<int>("ShoppingListId");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_Category");

            entity.HasIndex(i => i.Name).IsUnique(true);

            entity.Property(p => p.Name).IsRequired(true);
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_Subcategory");

            entity.Property(id => id.Id)
            .HasDefaultValueSql("NEXT VALUE FOR SubcategoryId");

            entity.HasIndex(i => i.Name).IsUnique(true);

            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.CategoryId).IsRequired(true);

            entity.HasOne(a => a.Category)
            .WithMany(b => b.Subcategories)
            .HasForeignKey(c => c.CategoryId)
            .HasConstraintName("FK_SubcategoryToCategory");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_Item");

            entity.Property(id => id.Id)
            .HasDefaultValueSql("NEXT VALUE FOR ItemId");

            entity.HasIndex(i => i.Name).IsUnique(true);

            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.SubcategoryId).IsRequired(true);
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_ShoppingList");

            entity.Property(id => id.Id)
            .HasDefaultValueSql("NEXT VALUE FOR ShoppingListId");

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

        modelBuilder.Entity<IdentityRole>().HasData(
            new { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
            new { Id = "User", Name = "User", NormalizedName = "USER" });

        modelBuilder.Entity<User>().HasData(
            new { Id = "SeededAdminUser", 
                Name = "Kağan", 
                Surname = "ASLAN", 
                UserName = "kaganaslan56@gmail.com", 
                NormalizedUserName = "KAGANASLAN56@GMAIL.COM", 
                Email = "kaganaslan56@gmail.com", 
                NormalizedEmail = "KAGANASLAN56@GMAIL.COM", 
                EmailConfirmed = false, 
                PasswordHash = "AQAAAAIAAYagAAAAEGn4vXbD7k9PMobT7cou5IVGEZfN8UXtaSxmmpX+yvTAiwDUibY+WX2YIUviYaHzzw==", 
                SecurityStamp = "II4GL3KQ55AN7XN5OTVETQLLSX7AF3H7", 
                ConcurrencyStamp = "f2099005-344e-4df6-bab6-63ead94562de", 
                PhoneNumber = "123", 
                PhoneNumberConfirmed = false, 
                TwoFactorEnabled = false, 
                LockoutEnabled = true, 
                AccessFailedCount = 1 });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new {RoleId = "Admin", UserId="SeededAdminUser"});

        base.OnModelCreating(modelBuilder);
    }
}
