using System;
using System.Collections.Generic;
using FilmesMoura.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesMoura.WebAPI.BdContextFilme;

public partial class FilmeContext : DbContext
{
    public FilmeContext()
    {
    }

    public FilmeContext(DbContextOptions<FilmeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Filme> Filmes { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=FilmeBD_Moura;Trusted_Connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.Idfilme).HasName("PK__Filme__4C3389CA01FC73AC");

            entity.HasOne(d => d.IdgeneroNavigation).WithMany(p => p.Filmes).HasConstraintName("FK__Filme__IDGenero__5EBF139D");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Idgenero).HasName("PK__Genero__40E9040FF2A58B56");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuario__523111699F9386E5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
