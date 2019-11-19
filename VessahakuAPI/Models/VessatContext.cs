using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VessahakuAPI.Models
{
    public partial class VessatContext : DbContext
    {
        public VessatContext()
        {
        }

        public VessatContext(DbContextOptions<VessatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kommentit> Kommentit { get; set; }
        public virtual DbSet<Käyttäjät> Käyttäjät { get; set; }
        public virtual DbSet<Wct> Wct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=Vessat;trusted_connection=true", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kommentit>(entity =>
            {
                entity.HasKey(e => e.KommenttiId)
                    .HasName("PK__Kommenti__7119F6A7A492C776");

                entity.Property(e => e.KommenttiId).HasColumnName("Kommentti_ID");

                entity.Property(e => e.KäyttäjäId).HasColumnName("Käyttäjä_ID");

                entity.Property(e => e.Lisätty)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sisältö).HasMaxLength(200);

                entity.Property(e => e.WcId).HasColumnName("WC_ID");

                entity.HasOne(d => d.Wc)
                    .WithMany(p => p.Kommentit)
                    .HasForeignKey(d => d.WcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Kommentit_WCt");
            });

            modelBuilder.Entity<Käyttäjät>(entity =>
            {
                entity.HasKey(e => e.KäyttäjäId)
                    .HasName("PK__Käyttäjä__914A7D9500C0321D");

                entity.Property(e => e.KäyttäjäId).HasColumnName("Käyttäjä_ID");

                entity.Property(e => e.Nimimerkki)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Salasana)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Sähköposti)
                    .IsRequired()
                    .HasMaxLength(320);
            });

            modelBuilder.Entity<Wct>(entity =>
            {
                entity.HasKey(e => e.WcId)
                    .HasName("PK__WCt__B59F4AC53CE39431");

                entity.ToTable("WCt");

                entity.Property(e => e.WcId).HasColumnName("WC_ID");

                entity.Property(e => e.Aukioloajat).HasMaxLength(70);

                entity.Property(e => e.Katuosoite)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Kaupunki)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Koodi).HasMaxLength(30);

                entity.Property(e => e.KäyttäjäId).HasColumnName("Käyttäjä_ID");

                entity.Property(e => e.Lisätty)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Muokattu).HasColumnType("datetime");

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Ohjeet).HasMaxLength(200);

                entity.Property(e => e.Postinro)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Sijainti).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
