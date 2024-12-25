using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaoyuanBIMAPI.Model.Data;

public partial class TaoyuanBimContext : DbContext
{
    public TaoyuanBimContext()
    {
    }

    public TaoyuanBimContext(DbContextOptions<TaoyuanBimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }

    public virtual DbSet<Layer> Layers { get; set; }

    public virtual DbSet<LayersGroup> LayersGroups { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=10.165.128.25;Database=TaoyuanBIM;User ID=taoyuanbim;Password=TaoyuanBIM;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.ToTable("Bookmark");

            entity.Property(e => e.BookmarkId).HasColumnName("bookmarkId");
            entity.Property(e => e.Base64img).HasColumnName("base64img");
            entity.Property(e => e.BookmarkName)
                .HasMaxLength(120)
                .HasColumnName("bookmarkName");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("createTime");
            entity.Property(e => e.DimensionMark).HasColumnName("dimensionMark");
            entity.Property(e => e.Dimention)
                .HasMaxLength(10)
                .HasColumnName("dimention");
            entity.Property(e => e.Heading)
                .HasColumnType("numeric(15, 10)")
                .HasColumnName("heading");
            entity.Property(e => e.Latitude)
                .HasColumnType("numeric(15, 10)")
                .HasColumnName("latitude");
            entity.Property(e => e.Layer).HasColumnName("layer");
            entity.Property(e => e.Longitude)
                .HasColumnType("numeric(15, 10)")
                .HasColumnName("longitude");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.Scale)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("scale");
            entity.Property(e => e.Slice).HasColumnName("slice");
            entity.Property(e => e.Tilt)
                .HasColumnType("numeric(15, 10)")
                .HasColumnName("tilt");
            entity.Property(e => e.UserId)
                .HasMaxLength(120)
                .HasColumnName("userId");
        });

        modelBuilder.Entity<Layer>(entity =>
        {
            entity.Property(e => e.LayerId)
                .HasMaxLength(60)
                .HasColumnName("layerId");
            entity.Property(e => e.GroupId)
                .HasMaxLength(60)
                .HasColumnName("groupId");
            entity.Property(e => e.LayerName)
                .HasMaxLength(60)
                .HasColumnName("layerName");
            entity.Property(e => e.LayerOrder).HasColumnName("layerOrder");
            entity.Property(e => e.LayerType)
                .HasMaxLength(30)
                .HasColumnName("layerType");
            entity.Property(e => e.Note)
                .HasMaxLength(150)
                .HasColumnName("note");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("url");
            entity.Property(e => e.UrlId)
                .HasMaxLength(30)
                .HasColumnName("urlId");

            entity.HasOne(d => d.Group).WithMany(p => p.Layers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Layers_LayersGroup");
        });

        modelBuilder.Entity<LayersGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId);

            entity.ToTable("LayersGroup");

            entity.Property(e => e.GroupId)
                .HasMaxLength(60)
                .HasColumnName("groupId");
            entity.Property(e => e.Dimention)
                .HasMaxLength(4)
                .HasColumnName("dimention");
            entity.Property(e => e.GroupName)
                .HasMaxLength(60)
                .HasColumnName("groupName");
            entity.Property(e => e.GroupOrder).HasColumnName("groupOrder");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
