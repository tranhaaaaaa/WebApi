
using Microsoft.EntityFrameworkCore;
namespace FinalApi.Models
{
    public partial class projectDemoContext : DbContext
    {
        public projectDemoContext()
        {
        }

        public projectDemoContext(DbContextOptions<projectDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Cartitem> Cartitems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Itemdetail> Itemdetails { get; set; } = null!;
        public virtual DbSet<Itemimage> Itemimages { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderdetail> Orderdetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Shipping> Shippings { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .Build();

            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(config.GetConnectionString("value"));

            }
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.District)
                    .HasMaxLength(255)
                    .HasColumnName("district");

                entity.Property(e => e.Province)
                    .HasMaxLength(255)
                    .HasColumnName("province");

                entity.Property(e => e.SpecificAdd)
                    .HasMaxLength(255)
                    .HasColumnName("specificAdd");

                entity.Property(e => e.Town)
                    .HasMaxLength(255)
                    .HasColumnName("town");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_customer_id");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("carts");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__carts__customer___4E88ABD4");
            });

            modelBuilder.Entity<Cartitem>(entity =>
            {
                entity.ToTable("cartitems");

                entity.Property(e => e.CartItemId).HasColumnName("cartItem_id");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__cartitems__cart___5629CD9C");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__cartitems__item___571DF1D5");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(255)
                    .HasColumnName("categoryDescription");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.AccessFailedCount).HasColumnName("accessFailedCount");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(255)
                    .HasColumnName("customerName");

                entity.Property(e => e.CustomerType).HasColumnName("customerType");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.LockOutEnable).HasColumnName("lockOutEnable");

                entity.Property(e => e.LockOutEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("lockOutEnd");

                entity.Property(e => e.Passwords)
                    .HasMaxLength(255)
                    .HasColumnName("passwords");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.ImportDate)
                    .HasColumnType("date")
                    .HasColumnName("importDate");

                entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(255)
                    .HasColumnName("itemDescription");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(255)
                    .HasColumnName("itemName");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__item__category_i__534D60F1");
            });

            modelBuilder.Entity<Itemdetail>(entity =>
            {
                entity.ToTable("itemdetails");

                entity.Property(e => e.ItemDetailId).HasColumnName("itemDetailId");

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ItemPrice).HasColumnName("itemPrice");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.Size).HasMaxLength(50);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Itemdetails)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__itemdetai__item___6B24EA82");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Itemdetails)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__itemdetai__shop___6C190EBB");
            });

            modelBuilder.Entity<Itemimage>(entity =>
            {
                entity.ToTable("itemimages");

                entity.Property(e => e.ItemImageId).HasColumnName("itemImageId");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ItemImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("itemImageURL");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Itemimages)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__itemimage__item___59FA5E80");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.Property(e => e.OverDueDate)
                    .HasColumnType("date")
                    .HasColumnName("overDueDate");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__orders__address___5FB337D6");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__orders__customer__60A75C0F");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.ToTable("orderdetails");

                entity.Property(e => e.OrderDetailId).HasColumnName("orderDetail_id");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.ItemDetailId).HasColumnName("itemDetailId");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.ItemDetail)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ItemDetailId)
                    .HasConstraintName("FK__orderdeta__itemD__6FE99F9F");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__orderdeta__order__70DDC3D8");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => new { e.PaymentCode, e.OrderId })
                    .HasName("PK__payments__31BC91D865D97E8A");

                entity.ToTable("payments");

                entity.Property(e => e.PaymentCode).HasColumnName("paymentCode");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(355)
                    .IsUnicode(false)
                    .HasColumnName("paymentMethod");

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paymentStatus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__payments__order___6383C8BA");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ShippingId })
                    .HasName("PK__shipping__C600D3738FBCF013");

                entity.ToTable("shippings");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ShippingId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("shipping_id");

                entity.Property(e => e.ShippingName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("shippingName");

                entity.Property(e => e.ShippingStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("shippingStatus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__shippings__order__73BA3083");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("shop");

                entity.Property(e => e.ShopId)
                    .ValueGeneratedNever()
                    .HasColumnName("shop_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("shopName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

 
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
