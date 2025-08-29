using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStore1.Models;

namespace Bookstore1.Data
{
    public class Bookstore1Context : DbContext
    {
        public Bookstore1Context (DbContextOptions<Bookstore1Context> options)
            : base(options)
        {
        }

        public DbSet<BookStore1.Models.Book> Book { get; set; } = default!;
        public DbSet<BookStore1.Models.Category> Category { get; set; } = default!;
        public DbSet<BookStore1.Models.Customer> Customer { get; set; } = default!;
        public DbSet<BookStore1.Models.Order> Order { get; set; } = default!;
        public DbSet<BookStore1.Models.User> User { get; set; } = default!;
        public DbSet<BookStore1.Models.Promotion> Promotion { get; set; } = default!;
        public DbSet<BookStore1.Models.OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<BookStore1.Models.Report> Report { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình mối quan hệ
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Promotion)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PromoId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Book)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(od => od.ISBN)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
