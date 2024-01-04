using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace B2S_REST_API.Domain;

public partial class Buy2SellContext : DbContext
{
    private readonly IConfiguration _configuration;

    public Buy2SellContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Buy2SellContext(DbContextOptions<Buy2SellContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BrandAlias> BrandAliases { get; set; }

    public virtual DbSet<ItemGroup> ItemGroups { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Db"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrdId).HasName("PK__Brand__4F6981A176C17CE3");

            entity.ToTable("Brand");

            entity.Property(e => e.BrdId).HasColumnName("brd_Id");
            entity.Property(e => e.BrdName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("brd_Name");
        });

        modelBuilder.Entity<BrandAlias>(entity =>
        {
            entity.HasKey(e => e.AliId).HasName("PK__BrandAli__FCDB161708BBB792");

            entity.ToTable("BrandAlias");

            entity.Property(e => e.AliId).HasColumnName("ali_Id");
            entity.Property(e => e.AliAlias)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ali_Alias");
            entity.Property(e => e.BrdId).HasColumnName("brd_Id");

            entity.HasOne(d => d.Brd).WithMany(p => p.BrandAliases)
                .HasForeignKey(d => d.BrdId)
                .HasConstraintName("FK__BrandAlia__brd_I__267ABA7A");
        });

        modelBuilder.Entity<ItemGroup>(entity =>
        {
            entity.HasKey(e => e.GrpId).HasName("PK__ItemGrou__F49FE793CABA2232");

            entity.ToTable("ItemGroup");

            entity.Property(e => e.GrpId).HasColumnName("grp_Id");
            entity.Property(e => e.GrpName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("grp_Name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PrdId).HasName("PK__Product__CE5DC560DB8C4266");

            entity.ToTable("Product");

            entity.Property(e => e.PrdId).HasColumnName("prd_Id");
            entity.Property(e => e.BrdId).HasColumnName("brd_Id");
            entity.Property(e => e.GrpId).HasColumnName("grp_Id");
            entity.Property(e => e.PrdEanGlr)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("prd_EAN_GLR");
            entity.Property(e => e.PrdName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("prd_Name");
            entity.Property(e => e.PrdProductNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("prd_ProductNumber");
            entity.Property(e => e.PrdProductText)
                .HasColumnType("text")
                .HasColumnName("prd_ProductText");
            entity.Property(e => e.PrdTypeNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("prd_TypeNumber");
            entity.Property(e => e.PrdUpc)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("prd_UPC");
            entity.Property(e => e.SpecJson)
                .HasColumnType("text")
                .HasColumnName("spec_Json");

            entity.HasOne(d => d.Brd).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrdId)
                .HasConstraintName("FK__Product__brd_Id__2C3393D0");

            entity.HasOne(d => d.Grp).WithMany(p => p.Products)
                .HasForeignKey(d => d.GrpId)
                .HasConstraintName("FK__Product__grp_Id__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
