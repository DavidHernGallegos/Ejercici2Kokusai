using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Datos;

public partial class BancoKokusaiContext : DbContext
{
    public BancoKokusaiContext()
    {
    }

    public BancoKokusaiContext(DbContextOptions<BancoKokusaiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuentahabiente> Cuentahabientes { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=BancoKokusai;Trusted_Connection=True; User ID=sa; Password=pass@word1;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuentahabiente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuentaha__3214EC071F337FA8");

            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3214EC07E6AE50DC");

            entity.Property(e => e.CantidadRetirada).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FechaHora).HasColumnType("datetime");

            entity.HasOne(d => d.IdCuentahabienteNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdCuentahabiente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacciones_Cuentahabientes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
