using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swiftshop.Database;

public class SwiftshopDbContext : IdentityDbContext<User>
{
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Subcategory> Subcategories { get; set; }
    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
    public virtual DbSet<ShoppingListContent> ShoppingListContents { get; set; }
    public virtual DbSet<ShoppingListHistory> ShoppingListHistories { get; set; }


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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_Product");

            entity.HasIndex(i => i.Name).IsUnique(true);

            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.SubcategoryId).IsRequired(true);
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

        modelBuilder.Entity<ShoppingListHistory>(entity =>
        {
            entity.HasKey(k => k.Id).HasName("PK_ShoppingListHistory");

            entity.Property(p => p.Name).IsRequired(true);
            entity.Property(p => p.UserId).IsRequired(true);

            entity.HasOne(a => a.User)
            .WithMany(b => b.ShoppingListHistories)
            .HasForeignKey(c => c.UserId)
            .HasConstraintName("FK_ShoppingListHistoryToUser");
        });

        modelBuilder.Entity<ShoppingListContent>(entity =>
        {
            entity.HasKey(a => new { a.ListId, a.ProductId }).HasName("CK_ListContent");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
            new { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
            new { Id = "User", Name = "User", NormalizedName = "USER" });

        modelBuilder.Entity<User>().HasData(
            new
            {
                Id = "SeededAdminUser",
                Name = "Swiftshop",
                Surname = "ADMIN",
                UserName = "admin@swiftshop.com",
                NormalizedUserName = "ADMIN@SWIFTSHOP.COM",
                Email = "admin@swiftshop.com",
                NormalizedEmail = "ADMIN@SWIFTSHOP.COM",
                EmailConfirmed = false,
                //Password = Swiftshop2023_
                PasswordHash = "AQAAAAIAAYagAAAAEKNcKIhrUy9MZKicHTVSsAjphHgHC2DzMJYKoUD0XTEtE4vbfG5c1SNAlJSsmoP7Hw==",
                SecurityStamp = "II4GL3KQ55AN7XN5OTVETQLLSX7AF3H7",
                ConcurrencyStamp = "f2099005-344e-4df6-bab6-63ead94562de",
                PhoneNumber = "123",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 1
            });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new { RoleId = "Admin", UserId = "SeededAdminUser" },
            new { RoleId = "User", UserId = "SeededAdminUser" });

        modelBuilder.Entity<Category>().HasData(
            new { Id = "50003d2c-30a0-4d82-a71b-ee20e912c177", Name = "Groceries" },
            new { Id = "beaa721b-96cc-430e-8659-cd0c3540b4c9", Name = "Home Care" },
            new { Id = "7995d582-7075-4301-8be0-662c8de8a659", Name = "Personal Care" }
            );

        modelBuilder.Entity<Subcategory>().HasData(
            new { Id = "8026e3a3-273e-4263-92e4-c16de21d1635", Name = "Beverages", CategoryId = "50003d2c-30a0-4d82-a71b-ee20e912c177" },
            new { Id = "cc531554-fccf-4190-a8ef-a80dcda4d8fb", Name = "Dairy", CategoryId = "50003d2c-30a0-4d82-a71b-ee20e912c177" },
            new { Id = "3ebec2ef-f3dc-484b-8f6f-034f2f06e472", Name = "Baked Goods", CategoryId = "50003d2c-30a0-4d82-a71b-ee20e912c177" },
            new { Id = "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90", Name = "Snacks", CategoryId = "50003d2c-30a0-4d82-a71b-ee20e912c177" },
            new { Id = "39082040-e1ca-4bbd-90cf-d5ea4b2a7b99", Name = "Foods", CategoryId = "50003d2c-30a0-4d82-a71b-ee20e912c177" },

            new { Id = "8167f531-03e3-41cf-9cad-4f75f841f8d1", Name = "Cleaning & Laundry", CategoryId = "beaa721b-96cc-430e-8659-cd0c3540b4c9" },
            new { Id = "f993ef29-8021-4244-8ea2-65f09a8278b0", Name = "Kitchen Supplies", CategoryId = "beaa721b-96cc-430e-8659-cd0c3540b4c9" },

            new { Id = "cf9528c4-618d-4203-a3c8-84f1be4531fa", Name = "Cosmetics", CategoryId = "7995d582-7075-4301-8be0-662c8de8a659" }
        );

        modelBuilder.Entity<Product>().HasData(
            new { Id = Guid.NewGuid().ToString(), Name = "Water", SubcategoryId = "8026e3a3-273e-4263-92e4-c16de21d1635", ProductImage = "https://img.imgyukle.com/2023/09/02/rZFJNp.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Soda", SubcategoryId = "8026e3a3-273e-4263-92e4-c16de21d1635", ProductImage = "https://img.imgyukle.com/2023/09/02/rZFEhv.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Coffee", SubcategoryId = "8026e3a3-273e-4263-92e4-c16de21d1635", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtZrs.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Tea", SubcategoryId = "8026e3a3-273e-4263-92e4-c16de21d1635", ProductImage = "https://img.imgyukle.com/2023/09/02/rZFRlx.png" },

            new { Id = Guid.NewGuid().ToString(), Name = "Milk", SubcategoryId = "cc531554-fccf-4190-a8ef-a80dcda4d8fb", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtwqe.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Yoghurt", SubcategoryId = "cc531554-fccf-4190-a8ef-a80dcda4d8fb", ProductImage = "https://img.imgyukle.com/2023/09/02/rZFQ2y.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Cheese", SubcategoryId = "cc531554-fccf-4190-a8ef-a80dcda4d8fb", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtFbb.png" },

            new { Id = Guid.NewGuid().ToString(), Name = "Bread", SubcategoryId = "3ebec2ef-f3dc-484b-8f6f-034f2f06e472", ProductImage = "https://img.imgyukle.com/2023/09/02/rZt30f.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Bagel", SubcategoryId = "3ebec2ef-f3dc-484b-8f6f-034f2f06e472", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtfNR.png" },

            new { Id = Guid.NewGuid().ToString(), Name = "Chocolate", SubcategoryId = "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtbhQ.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Chips", SubcategoryId = "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90", ProductImage = "https://img.imgyukle.com/2023/09/02/rZteqI.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Biscuits", SubcategoryId = "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90", ProductImage = "https://img.imgyukle.com/2023/09/02/rZttGS.png" },

            new { Id = Guid.NewGuid().ToString(), Name = "Pasta", SubcategoryId = "39082040-e1ca-4bbd-90cf-d5ea4b2a7b99", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtAIP.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Oil", SubcategoryId = "39082040-e1ca-4bbd-90cf-d5ea4b2a7b99", ProductImage = "https://img.imgyukle.com/2023/09/02/rZt07N.png" },

            new { Id = Guid.NewGuid().ToString(), Name = "Soap", SubcategoryId = "8167f531-03e3-41cf-9cad-4f75f841f8d1", ProductImage = "https://img.imgyukle.com/2023/09/02/rZFor0.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Bleach", SubcategoryId = "8167f531-03e3-41cf-9cad-4f75f841f8d1", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtl5G.png" },

            new { Id = Guid.NewGuid().ToString(), Name = "Trash Bag", SubcategoryId = "f993ef29-8021-4244-8ea2-65f09a8278b0", ProductImage = "https://img.imgyukle.com/2023/09/02/rZFnE6.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Dish Soap", SubcategoryId = "f993ef29-8021-4244-8ea2-65f09a8278b0", ProductImage = "https://img.imgyukle.com/2023/09/02/rZt9Ec.png" },

            new { Id = Guid.NewGuid().ToString(), Name = "Deodorant", SubcategoryId = "cf9528c4-618d-4203-a3c8-84f1be4531fa", ProductImage = "https://img.imgyukle.com/2023/09/02/rZt48t.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Shampoo", SubcategoryId = "cf9528c4-618d-4203-a3c8-84f1be4531fa", ProductImage = "https://img.imgyukle.com/2023/09/02/rZtdGq.png" },
            new { Id = Guid.NewGuid().ToString(), Name = "Shaving Cream", SubcategoryId = "cf9528c4-618d-4203-a3c8-84f1be4531fa", ProductImage = "https://img.imgyukle.com/2023/09/02/rZFP4Y.png" }
        );

        base.OnModelCreating(modelBuilder);
    }
}
