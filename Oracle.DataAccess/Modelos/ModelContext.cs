using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Oracle.DataAccess.Modelos;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //  => optionsBuilder.UseOracle("User Id=DB2;Password=1234;Data Source=localhost:1521/db2;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("DB2")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_USER_ID");

            entity.ToTable("USUARIO");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Papellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PAPELLIDO");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PNOMBRE");
            entity.Property(e => e.Sapellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SAPELLIDO");
            entity.Property(e => e.Snombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SNOMBRE");
            entity.Property(e => e.Tnombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TNOMBRE");
        });
        modelBuilder.HasSequence("USUARIO_ID_SEC");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
