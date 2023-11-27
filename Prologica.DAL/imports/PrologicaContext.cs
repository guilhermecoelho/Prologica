using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prologica.DAL.imports;

public partial class PrologicaContext : DbContext
{
    public PrologicaContext()
    {
    }

    public PrologicaContext(DbContextOptions<PrologicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Console> Consoles { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Pooling=true;Database=Prologica;User Id=postgres;Password=Postgres2018!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Console>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Consoles_pkey");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Games_pkey");

            entity.HasOne(d => d.Console).WithMany(p => p.Games)
                .HasForeignKey(d => d.ConsoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Game_Console_gkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
