

using Microsoft.EntityFrameworkCore;
using ReviewFood.Models;

namespace Food.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BaiViet> BaiViets { get; set; }

        public DbSet<BaiViet_DanhMuc> BaiViet_DanhMuc { get; set;}

        public DbSet<DanhGia> DanhGias { get; set; }

        //  public DbSet<DanhGia_BaiViet> DanhGia_BaiViet { get; set; }

        public DbSet<DanhMuc> DanhMucs { get; set; }

        public DbSet<DanhMucCha> DanhMucChas { get; set; }

        public DbSet<TaiKhoan> TaiKhoan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiViet_DanhMuc>()
                .HasKey(x => new { x.MaTinTuc, x.MaDanhMuc });
                            
            modelBuilder.Entity<DanhGia>()
                 .HasOne(c => c.BaiViet)
                 .WithMany(c => c.DanhGias)
                 .HasForeignKey(c => c.BaiVietId);
          
            modelBuilder.Entity<DanhGia>()
                    .HasKey(x => new { x.Id, x.IdTaiKhoan, x.BaiVietId });
            modelBuilder.Entity<BaiViet>()
                .HasOne(b => b.DanhMuc)
                .WithMany(b => b.BaiViets)
                .HasForeignKey(b => b.DanhMucId);
            modelBuilder.Entity<BaiViet>()
                 .HasOne(c => c.TaiKhoan)
                 .WithMany(c => c.BaiViets)
                 .HasForeignKey(c => c.IdTaiKhoan);
            modelBuilder.Entity<DanhMuc>()
                .HasOne(d => d.DanhMucCha)
                .WithMany(d => d.DanhMucs)
                .HasForeignKey(d => d.IdDanhMucCha);



        }
    }
}