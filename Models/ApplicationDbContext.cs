using Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<Executor> Executors { get; set; } = null!;
        public virtual DbSet<Good> Goods { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Nomenclature> Nomenclatures { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestGood> RequestGoods { get; set; } = null!;
        public virtual DbSet<Stage> Stages { get; set; } = null!;
        public virtual DbSet<CartGood> CartGoods { get; set; } = null!;
        public virtual DbSet<LikedGoodNomenclature> LikedGoodNomenclatures { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.DeliveryName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Executor>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.HasOne(d => d.IdNomNavigation)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.IdNom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Goods__IdNom__412EB0B6");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdReqNavigation)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.IdReq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History__IdReq__571DF1D5");

                entity.HasOne(d => d.IdStageNavigation)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.IdStage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History__IdStage__5812160E");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.MimeType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNomNavigation)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.IdNom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Images__IdNom__440B1D61");
            });

            modelBuilder.Entity<Nomenclature>(entity =>
            {
                entity.ToTable("Nomenclature");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBrandNavigation)
                    .WithMany(p => p.Nomenclatures)
                    .HasForeignKey(d => d.IdBrand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nomenclat__IdBra__3D5E1FD2");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Nomenclatures)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nomenclat__IdCat__3C69FB99");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.Adress).IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.FullPrice)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser).HasMaxLength(450);

                entity.HasOne(d => d.IdDeliveryNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdDelivery)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Requests__IdDeli__4BAC3F29");

                entity.HasOne(d => d.IdExecNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdExec)
                    .HasConstraintName("FK__Requests__IdExec__4CA06362");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Requests__IdUser__4AB81AF0");
            });

            modelBuilder.Entity<RequestGood>(entity =>
            {
                entity.ToTable("Request_Good");

                entity.HasOne(d => d.IdGoodNavigation)
                    .WithMany(p => p.RequestGoods)
                    .HasForeignKey(d => d.IdGood)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request_G__IdGoo__5165187F");

                entity.HasOne(d => d.IdReqNavigation)
                    .WithMany(p => p.RequestGoods)
                    .HasForeignKey(d => d.IdReq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request_G__IdReq__5070F446");
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.Property(e => e.StageName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LikedGoodNomenclature>(entity =>
            {
                entity.ToTable("LikedGood_Nomenclature");

                entity.Property(e => e.IdUser).HasMaxLength(450);
                entity.HasOne(d => d.IdNomNavigation)
                    .WithMany(p => p.LikedGoodNomenclatures)
                    .HasForeignKey(d => d.IdNom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LikedGood_N__IdNom__6165187F");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.LikedGoodNomenclatures)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LikedGood_N__IdLikedGood__6070F446");
            });
            modelBuilder.Entity<CartGood>(entity =>
            {
                entity.ToTable("Cart_Good");
                entity.Property(e => e.IdUser).HasMaxLength(450);

                entity.HasOne(d => d.IdGoodNavigation)
                    .WithMany(p => p.CartGoods)
                    .HasForeignKey(d => d.IdGood)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart_G__IdGoo__7165187F");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.CartGoods)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart_G__IdGoo__7070F446");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}