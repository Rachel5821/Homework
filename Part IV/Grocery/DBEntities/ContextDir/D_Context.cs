using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBEntities.Models
{
    public partial class D_Context : DbContext
    {
        public D_Context()
        {
        }

        public D_Context(DbContextOptions<D_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemInOrder> ItemInOrders { get; set; }
        public virtual DbSet<Merchandise> Merchandises { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierAndmerchandise> SupplierAndmerchandises { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // מיקום של חיבור למסד נתונים. 
            // יש לשמור את המידע הרגיש בקובץ קונפיגורציה ולא בקוד ישיר.
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-8823H7O\\SQLEXPRESS;Initial Catalog=grocery;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId).HasName("PK__Item__52020FDD97CD5155");

                entity.ToTable("Item");

                entity.Property(e => e.ItemId).HasColumnName("item_id");
                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration_date");
                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("item_name");
            });

            modelBuilder.Entity<ItemInOrder>(entity =>
            {
                entity.HasKey(e => e.ItemInOrderId).HasName("PK__ItemInOr__B2EA5A0B962F5A38");

                entity.ToTable("ItemInOrder");

                entity.Property(e => e.ItemInOrderId).HasColumnName("ItemInOrder_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ItemInOrder_OrderDetail"); 
            });


            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.HasKey(e => e.MerchandiseId).HasName("PK__Merchand__D9629325A511EE9B");

                entity.ToTable("Merchandise");

                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");
                entity.Property(e => e.ItemId).HasColumnName("item_id");
                entity.Property(e => e.MinimumQuantity).HasColumnName("Minimum_quantity");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Merchandises)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__Merchandi__item___3B75D760");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PK__OrderDet__465962294CCC97B7");

                entity.ToTable("OrderDetail"); // שם הטבלה החדש במסד נתונים

                entity.Property(e => e.OrderId).HasColumnName("OrderId");
                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseId");
                entity.Property(e => e.order_status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("order_status");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
                entity.Property(e => e.SupplierId).HasColumnName("SupplierId");
            });


            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__6EE594E8007DE2EC");

                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.ManufacturerName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("manufacturer_name");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");
                entity.Property(e => e.RepresentativeName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("representative_name");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Supplier_User");
            });

            modelBuilder.Entity<SupplierAndmerchandise>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("SupplierAndmerchandise");

                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27AD05F9A3");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IsManager).HasColumnName("IS_MANAGER");
                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
