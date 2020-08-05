using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dell_hospital.Models
{
    public partial class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consultas> Consultas { get; set; }
        public virtual DbSet<Enfermeiros> Enfermeiros { get; set; }
        public virtual DbSet<Especialidades> Especialidades { get; set; }
        public virtual DbSet<Medicos> Medicos { get; set; }
        public virtual DbSet<Pacientes> Pacientes { get; set; }
        public virtual DbSet<Triagem> Triagem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=DESKTOP-QR5M9H5;initial catalog=Hospital;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultas>(entity =>
            {
                entity.HasKey(e => e.CodConsultas)
                    .HasName("PK__Consulta__AB89BEAB7D0B622C");

                entity.HasIndex(e => new { e.Cpf, e.DataConsulta, e.Crm, e.Coren, e.CodTriagem })
                    .HasName("ck_marcado")
                    .IsUnique();

                entity.Property(e => e.CodConsultas).HasColumnName("Cod_Consultas");

                entity.Property(e => e.CodTriagem).HasColumnName("Cod_Triagem");

                entity.Property(e => e.Coren)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Crm)
                    .IsRequired()
                    .HasColumnName("CRM")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DataConsulta).HasColumnType("datetime");

                entity.HasOne(d => d.CodTriagemNavigation)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.CodTriagem)
                    .HasConstraintName("FK_TRIAGEM");

                entity.HasOne(d => d.CorenNavigation)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.Coren)
                    .HasConstraintName("FK_ENFERMEIROS");

                entity.HasOne(d => d.CpfNavigation)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.Cpf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PACIENTES");

                entity.HasOne(d => d.CrmNavigation)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.Crm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MEDICOS");
            });

            modelBuilder.Entity<Enfermeiros>(entity =>
            {
                entity.HasKey(e => e.Coren)
                    .HasName("PK__Enfermei__F7D2C3E98FFE1C85");

                entity.Property(e => e.Coren)
                    .HasColumnName("COREN")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Especialidades>(entity =>
            {
                entity.HasKey(e => e.CodEspecialidade)
                    .HasName("PK__Especial__0EB6E8550930863F");

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Especial__7D8FE3B20CFA1462")
                    .IsUnique();

                entity.Property(e => e.CodEspecialidade).HasColumnName("Cod_especialidade");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValorConsulta).HasColumnType("numeric(4, 0)");
            });

            modelBuilder.Entity<Medicos>(entity =>
            {
                entity.HasKey(e => e.Crm)
                    .HasName("PK__Medicos__C1F887FE43619D62");

                entity.Property(e => e.Crm)
                    .HasColumnName("CRM")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.CodEspecialidade).HasColumnName("Cod_especialidade");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodEspecialidadeNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.CodEspecialidade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ESPECIALIDADES");
            });

            modelBuilder.Entity<Pacientes>(entity =>
            {
                entity.HasKey(e => e.Cpf)
                    .HasName("PK__Paciente__C1F89730EB4550E3");

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnName("SEXO")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Triagem>(entity =>
            {
                entity.HasKey(e => e.CodTriagem)
                    .HasName("PK__Triagem__49128E5642A68381");

                entity.Property(e => e.CodTriagem).HasColumnName("Cod_triagem");

                entity.Property(e => e.Coren)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DataConsulta).HasColumnType("datetime");

                entity.Property(e => e.DescricaoPaciente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prioridade)
                    .HasColumnName("prioridade")
                    .HasColumnType("numeric(1, 0)");

                entity.HasOne(d => d.CorenNavigation)
                    .WithMany(p => p.Triagem)
                    .HasForeignKey(d => d.Coren)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COREN");

                entity.HasOne(d => d.CpfNavigation)
                    .WithMany(p => p.Triagem)
                    .HasForeignKey(d => d.Cpf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CPF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
