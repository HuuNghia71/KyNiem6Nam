using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KyNiem6Nam.Models;

public partial class KyNiem6NamContext : DbContext
{
    public KyNiem6NamContext()
    {
    }

    public KyNiem6NamContext(DbContextOptions<KyNiem6NamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Letter> Letters { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<SavedPlace> SavedPlaces { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=nghia;Initial Catalog= KyNiem6Nam;Integrated Security=True;TrustServerCertificate=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Letter>(entity =>
        {
            entity.HasKey(e => e.LetterId).HasName("PK__Letters__AE46E8F182F22AFB");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsLiked).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Sender).WithMany(p => p.Letters)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Letters_Users");
        });

        modelBuilder.Entity<Music>(entity =>
        {
            entity.HasKey(e => e.MusicId).HasName("PK__Musics__11F8400019AAADB1");

            entity.Property(e => e.AudioUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.SongTitle).HasMaxLength(200);

            entity.HasOne(d => d.AddedBy).WithMany(p => p.Musics)
                .HasForeignKey(d => d.AddedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Musics_Users");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Photos__21B7B5E23BABB953");

            entity.Property(e => e.Caption).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Uploader).WithMany(p => p.Photos)
                .HasForeignKey(d => d.UploaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_Users");
        });

        modelBuilder.Entity<SavedPlace>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__SavedPla__D5222B6EDB8FECC4");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.PlaceName).HasMaxLength(200);

            entity.HasOne(d => d.AddedBy).WithMany(p => p.SavedPlaces)
                .HasForeignKey(d => d.AddedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SavedPlaces_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CAF32E435");

            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LastLocationUpdate).HasColumnType("datetime");
            entity.Property(e => e.Passcode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pin).HasDefaultValue(100);
            entity.Property(e => e.ThoiTiet).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
