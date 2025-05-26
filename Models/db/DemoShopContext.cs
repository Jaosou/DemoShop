using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoShop.Models.db;

public partial class DemoShopContext : DbContext
{
    public DemoShopContext()
    {
    }

    public DemoShopContext(DbContextOptions<DemoShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleBook> SaleBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DemoShop;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.BookId)
                .HasMaxLength(50)
                .HasColumnName("bookID");
            entity.Property(e => e.BookCost).HasColumnName("bookCost");
            entity.Property(e => e.BookName)
                .HasMaxLength(50)
                .HasColumnName("bookName");
            entity.Property(e => e.BookPrice).HasColumnName("bookPrice");
            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.PublishId).HasColumnName("publishID");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Category");

            entity.HasOne(d => d.Publish).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Publisher");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B82EDBC98E");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublishId);

            entity.ToTable("Publisher");

            entity.Property(e => e.PublishId).HasColumnName("publishID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.ContactName)
                .HasMaxLength(50)
                .HasColumnName("contactName");
            entity.Property(e => e.PublishName)
                .HasMaxLength(50)
                .HasColumnName("publishName");
            entity.Property(e => e.Telephone)
                .HasMaxLength(12)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sale__1EE3C41F51A29066");

            entity.ToTable("Sale");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.SaleDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sale__CustomerID__5EBF139D");
        });

        modelBuilder.Entity<SaleBook>(entity =>
        {
            entity.HasKey(e => e.SaleBookId).HasName("PK__SaleBook__65C1A0277C0A25BC");

            entity.ToTable("SaleBook");

            entity.Property(e => e.SaleBookId)
                .ValueGeneratedNever()
                .HasColumnName("SaleBookID");
            entity.Property(e => e.BookId)
                .HasMaxLength(50)
                .HasColumnName("BookID");
            entity.Property(e => e.SaleId).HasColumnName("SaleID");

            entity.HasOne(d => d.Book).WithMany(p => p.SaleBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleBook__BookID__628FA481");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleBooks)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleBook__SaleID__619B8048");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
