using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI
{
    public partial class FootballDataContext : DbContext
    {
        public FootballDataContext()
        {
        }

        public FootballDataContext(DbContextOptions<FootballDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<MatchOdd> MatchOdds { get; set; }
        public virtual DbSet<SportDescr> SportDescrs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Match");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MatchDate).HasColumnType("date");

                entity.Property(e => e.MatchTime).HasColumnType("time(0)");

                entity.Property(e => e.TeamA)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TeamB)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.SportNavigation)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.Sport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Match_SportDescr");
            });

            modelBuilder.Entity<MatchOdd>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Odd).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.Specifier)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchOdds)
                    .HasForeignKey(d => d.MatchId)
                    .HasConstraintName("FK_MatchOdds_Match");
            });

            modelBuilder.Entity<SportDescr>(entity =>
            {
                entity.ToTable("SportDescr");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}