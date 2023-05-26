using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class CongTTDHLuatContext : DbContext
    {
        public CongTTDHLuatContext()
        {
        }

        public CongTTDHLuatContext(DbContextOptions<CongTTDHLuatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BaCongKhai> BaCongKhais { get; set; }
        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<CoCauToChuc> CoCauToChucs { get; set; }
        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<GioiThieu> GioiThieus { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<Logo> Logos { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<Phong> Phongs { get; set; }
        public virtual DbSet<QuyDinhSv> QuyDinhSvs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<ThongBaoKhoa> ThongBaoKhoas { get; set; }
        public virtual DbSet<ThongBaoPhong> ThongBaoPhongs { get; set; }
        public virtual DbSet<ThongBaoSv> ThongBaoSvs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DUNGCOOL\\SQLEXPRESS;Database=CongTTDHLuat;Integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.MaAdmin)
                    .HasName("PK__Admin__49341E385B22A3D1");

                entity.ToTable("Admin");

                entity.HasIndex(e => e.Email, "UQ__Admin__A9D10534CCCEE571")
                    .IsUnique();

                entity.HasIndex(e => e.PasswordAd, "UQ__Admin__EA7BB3C399DCA7DE")
                    .IsUnique();

                entity.Property(e => e.AvatarAd).HasMaxLength(100);

                entity.Property(e => e.DanToc).HasMaxLength(30);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordAd).HasMaxLength(100);

                entity.Property(e => e.QuocTich).HasMaxLength(30);

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.TonGiao).HasMaxLength(30);

                entity.Property(e => e.UsernameAd).HasMaxLength(100);
            });

            modelBuilder.Entity<BaCongKhai>(entity =>
            {
                entity.HasKey(e => e.MaBck)
                    .HasName("PK__BaCongKh__35217F3210CA8989");

                entity.ToTable("BaCongKhai");

                entity.Property(e => e.MaBck)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaBCK")
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenBck).HasColumnName("TenBCK");
            });

            modelBuilder.Entity<BaiViet>(entity =>
            {
                entity.HasKey(e => e.MaBv)
                    .HasName("PK__BaiViet__27247595A61DF0C6");

                entity.ToTable("BaiViet");

                entity.Property(e => e.MaBv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaBV")
                    .IsFixedLength(true);

                entity.Property(e => e.AnhBv).HasColumnName("AnhBV");

                entity.Property(e => e.MaChuDe)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenBv).HasColumnName("TenBV");

                entity.HasOne(d => d.MaChuDeNavigation)
                    .WithMany(p => p.BaiViets)
                    .HasForeignKey(d => d.MaChuDe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiViet__MaChuDe__45F365D3");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.MaBanner)
                    .HasName("PK__Banner__508B4A497130F25D");

                entity.ToTable("Banner");

                entity.Property(e => e.AnhBanner).IsUnicode(false);

                entity.Property(e => e.MoTaNgan).HasMaxLength(100);

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.TenBanner).HasMaxLength(50);
            });

            modelBuilder.Entity<ChuDe>(entity =>
            {
                entity.HasKey(e => e.MaChuDe)
                    .HasName("PK__ChuDe__35854511422434B4");

                entity.ToTable("ChuDe");

                entity.Property(e => e.MaChuDe)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TenCd).HasColumnName("TenCD");
            });

            modelBuilder.Entity<CoCauToChuc>(entity =>
            {
                entity.HasKey(e => e.MaTc)
                    .HasName("PK__CoCauToC__27250068E2F9A2C7");

                entity.ToTable("CoCauToChuc");

                entity.Property(e => e.MaTc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTC")
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenTc).HasColumnName("TenTC");
            });

            modelBuilder.Entity<GiangVien>(entity =>
            {
                entity.HasKey(e => e.MaGv)
                    .HasName("PK__GiangVie__2725AEF3FEC17F40");

                entity.ToTable("GiangVien");

                entity.HasIndex(e => e.Email, "UQ__GiangVie__A9D1053479FDECFE")
                    .IsUnique();

                entity.Property(e => e.MaGv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaGV")
                    .IsFixedLength(true);

                entity.Property(e => e.AnhGv).HasColumnName("AnhGV");

                entity.Property(e => e.Cmnd).HasColumnName("CMND");

                entity.Property(e => e.DanToc).HasMaxLength(30);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.HoTen).HasMaxLength(30);

                entity.Property(e => e.MaKhoa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.NoiSinh)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordGv)
                    .HasMaxLength(30)
                    .HasColumnName("PasswordGV");

                entity.Property(e => e.QuocTich).HasMaxLength(30);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("SDT")
                    .IsFixedLength(true);

                entity.Property(e => e.TonGiao).HasMaxLength(30);

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.GiangViens)
                    .HasForeignKey(d => d.MaKhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GiangVien__MaKho__32E0915F");
            });

            modelBuilder.Entity<GioiThieu>(entity =>
            {
                entity.HasKey(e => e.MaGt)
                    .HasName("PK__GioiThie__2725AEF1C9A77444");

                entity.ToTable("GioiThieu");

                entity.Property(e => e.MaGt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaGT")
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenGt).HasColumnName("TenGT");
            });

            modelBuilder.Entity<Khoa>(entity =>
            {
                entity.HasKey(e => e.MaKhoa)
                    .HasName("PK__Khoa__65390405E058B58F");

                entity.ToTable("Khoa");

                entity.HasIndex(e => e.EmailKhoa, "UQ__Khoa__A7CDAC9A437F7955")
                    .IsUnique();

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.AvatarKhoa).IsUnicode(false);

                entity.Property(e => e.EmailKhoa)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sdtkhoa).HasColumnName("SDTKhoa");

                entity.Property(e => e.SoGv).HasColumnName("SoGV");

                entity.Property(e => e.SoSv).HasColumnName("SoSV");
            });

            modelBuilder.Entity<Logo>(entity =>
            {
                entity.HasKey(e => e.MaLogo)
                    .HasName("PK__Logo__730A042B19079A36");

                entity.ToTable("Logo");

                entity.Property(e => e.AvatarLogo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao).HasColumnType("datetime");
            });

            modelBuilder.Entity<Lop>(entity =>
            {
                entity.HasKey(e => e.MaLop)
                    .HasName("PK__Lop__3B98D27320B40B8D");

                entity.ToTable("Lop");

                entity.Property(e => e.MaLop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaKhoa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TenLop).HasMaxLength(50);

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.Lops)
                    .HasForeignKey(d => d.MaKhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lop__MaKhoa__2F10007B");
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.MaPhong)
                    .HasName("PK__Phong__20BD5E5BAF8178B1");

                entity.ToTable("Phong");

                entity.Property(e => e.MaPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TenPhong).HasMaxLength(1);
            });

            modelBuilder.Entity<QuyDinhSv>(entity =>
            {
                entity.HasKey(e => e.MaQdsv)
                    .HasName("PK__QuyDinhS__8C120C764865654C");

                entity.ToTable("QuyDinhSV");

                entity.Property(e => e.MaQdsv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaQDSV")
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenQd).HasColumnName("TenQD");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSv)
                    .HasName("PK__SinhVien__2725081A2BFA76FB");

                entity.ToTable("SinhVien");

                entity.HasIndex(e => e.EmailSv, "UQ__SinhVien__7ED9AB0FEE930504")
                    .IsUnique();

                entity.Property(e => e.MaSv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaSV")
                    .IsFixedLength(true);

                entity.Property(e => e.AnhSv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AnhSV");

                entity.Property(e => e.Cmnd).HasColumnName("CMND");

                entity.Property(e => e.DanToc).HasMaxLength(30);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.EmailSv)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EmailSV");

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.MaKhoa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaLop)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NoiSinh)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordSv)
                    .IsUnicode(false)
                    .HasColumnName("PasswordSV");

                entity.Property(e => e.QuocTich).HasMaxLength(30);

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.SdtphuHuynh).HasColumnName("SDTPhuHuynh");

                entity.Property(e => e.TenSv)
                    .HasMaxLength(30)
                    .HasColumnName("TenSV");

                entity.Property(e => e.TonGiao).HasMaxLength(30);

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.SinhViens)
                    .HasForeignKey(d => d.MaKhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SinhVien__MaKhoa__36B12243");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.SinhViens)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SinhVien__MaLop__37A5467C");
            });

            modelBuilder.Entity<ThongBaoKhoa>(entity =>
            {
                entity.HasKey(e => e.MaTbkhoa)
                    .HasName("PK__ThongBao__6CC0B3D3463FA78B");

                entity.ToTable("ThongBaoKhoa");

                entity.Property(e => e.MaTbkhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTBKhoa")
                    .IsFixedLength(true);

                entity.Property(e => e.AnhTb).HasColumnName("AnhTB");

                entity.Property(e => e.MaKhoa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenTb).HasColumnName("TenTB");

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.ThongBaoKhoas)
                    .HasForeignKey(d => d.MaKhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongBaoK__MaKho__3C69FB99");
            });

            modelBuilder.Entity<ThongBaoPhong>(entity =>
            {
                entity.HasKey(e => e.MaTbphong)
                    .HasName("PK__ThongBao__899052B33F2064E4");

                entity.ToTable("ThongBaoPhong");

                entity.Property(e => e.MaTbphong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTBPhong")
                    .IsFixedLength(true);

                entity.Property(e => e.AnhTb).HasColumnName("AnhTB");

                entity.Property(e => e.MaPhong)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenTb).HasColumnName("TenTB");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.ThongBaoPhongs)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongBaoP__MaPho__3F466844");
            });

            modelBuilder.Entity<ThongBaoSv>(entity =>
            {
                entity.HasKey(e => e.MaTbsv)
                    .HasName("PK__ThongBao__B23589A764703986");

                entity.ToTable("ThongBaoSV");

                entity.Property(e => e.MaTbsv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTBSV")
                    .IsFixedLength(true);

                entity.Property(e => e.AnhTb).HasColumnName("AnhTB");

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TenTb).HasColumnName("TenTB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
