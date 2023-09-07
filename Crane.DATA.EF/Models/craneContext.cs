using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Crane.DATA.EF.Models
{
    public partial class craneContext : DbContext
    {
        public craneContext()
        {
        }

        public craneContext(DbContextOptions<craneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Beer> Beers { get; set; } = null!;
        public virtual DbSet<BeerType> BeerTypes { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Merch> Merches { get; set; } = null!;
        public virtual DbSet<MerchType> MerchTypes { get; set; } = null!;
        public virtual DbSet<TeamMember> TeamMembers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;database=crane;trusted_connection=true;multipleactiveresultsets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Beer>(entity =>
            {
                entity.Property(e => e.BeerId).HasColumnName("BeerID");

                entity.Property(e => e.BeerAbv)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("BeerABV");

                entity.Property(e => e.BeerDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BeerImage)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.BeerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BeerTypeId).HasColumnName("BeerTypeID");

                entity.HasOne(d => d.BeerType)
                    .WithMany(p => p.Beers)
                    .HasForeignKey(d => d.BeerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beers_BeerTypes");
            });

            modelBuilder.Entity<BeerType>(entity =>
            {
                entity.Property(e => e.BeerTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("BeerTypeID");

                entity.Property(e => e.BeerTypeDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BeerTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId)
                    .ValueGeneratedNever()
                    .HasColumnName("EventID");

                entity.Property(e => e.EventDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EventDescripton).IsUnicode(false);

                entity.Property(e => e.EventImage)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.EventName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventTime)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Merch>(entity =>
            {
                entity.ToTable("Merch");

                entity.Property(e => e.MerchId)
                    .ValueGeneratedNever()
                    .HasColumnName("MerchID");

                entity.Property(e => e.MerchDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MerchImage)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.MerchName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MerchPrice).HasColumnType("money");

                entity.Property(e => e.MerchTypeId).HasColumnName("MerchTypeID");

                entity.HasOne(d => d.MerchType)
                    .WithMany(p => p.Merches)
                    .HasForeignKey(d => d.MerchTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Merch_MerchType");
            });

            modelBuilder.Entity<MerchType>(entity =>
            {
                entity.ToTable("MerchType");

                entity.Property(e => e.MerchTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("MerchTypeID");

                entity.Property(e => e.MerchDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MerchName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.Property(e => e.TeamMemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("TeamMemberID");

                entity.Property(e => e.TeamMemberDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TeamMemberImage)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.TeamMemberName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeamMemberTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
