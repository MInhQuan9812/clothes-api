using clothes.api.Instrafructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Instrafructure.Context
{
    public class ClothesContext : DbContext
    {
        public ClothesContext(DbContextOptions<ClothesContext> options) : base(options) { }


        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<OptionValue> OptionValue { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserModel(modelBuilder);
            ConfigureProductModel(modelBuilder);
            ConfigureCart(modelBuilder);
            ConfigureCartItem(modelBuilder);
            ConfigureOrderModel(modelBuilder);
            ConfigureOrderDetaill(modelBuilder);
            ConfigureProductOptionValue(modelBuilder);
            ConfigureWishlist(modelBuilder);
        }

        private void ConfigureUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable(nameof(User))
                .HasKey(e => e.Id);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Cart)
                .WithOne(x => x.Customer)
                .HasForeignKey<Cart>(x => x.CustomerId);
        }
        private void ConfigureOrderDetaill(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .ToTable(nameof(OrderDetail))
                .HasKey(e => e.Id);

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(x => x.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderDetail>()
                       .HasOne(x => x.Product)
                       .WithMany()
                       .OnDelete(DeleteBehavior.Restrict);

        }
        private void ConfigureProductModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable(nameof(Product))
                .HasKey(e => e.Id);


            modelBuilder.Entity<Product>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Product)
                .HasForeignKey(x => x.CategoryId);
        }


        private void ConfigureOrderModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .ToTable(nameof(Order))
                .HasKey(e => e.Id);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Promotion)
                .WithMany(x => x.Order)
                .HasForeignKey(x => x.PromotionId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Payment)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.PaymentId);
        }

        private void ConfigureCart(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .ToTable(nameof(Cart))
                .HasKey(e => e.Id);
        }

        private void ConfigureCartItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .ToTable(nameof(CartItem))
                .HasKey(e => e.Id);

            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.Cart)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.Product)
                .WithOne(x => x.CartItem)
                .HasForeignKey<CartItem>(x => x.ProductId);
        }

        private void ConfigureWishlist(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wishlist>()
                .ToTable(nameof(Wishlist))
                .HasKey(e => e.Id);

            modelBuilder.Entity<Wishlist>()
                .HasOne(x => x.Product)
                .WithOne(x => x.Wishlist)
                .HasForeignKey<Wishlist>(x => x.ProductId);

            modelBuilder.Entity<Wishlist>()
                .HasOne(x => x.User)
                .WithMany(x => x.Wishlists)
                .HasForeignKey(x => x.UserId);

        }



        public void ConfigureProductOptionValue(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOptionValue>()
                .ToTable(nameof(ProductOptionValue))
                .HasKey(x => x.Id);

            modelBuilder.Entity<ProductOptionValue>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductOptionValues)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.ClientNoAction);


            modelBuilder.Entity<ProductOptionValue>()
                .HasOne(x => x.Option)
                .WithMany(x => x.ProductOptionValues)
                .HasForeignKey(x => x.OptionId)
                .OnDelete(DeleteBehavior.ClientNoAction);


            modelBuilder.Entity<ProductOptionValue>()
                .HasOne(x => x.OptionValue)
                .WithMany(x => x.ProductOptionValues)
                .HasForeignKey(x => x.OptionValueId);
        }
    }
}
