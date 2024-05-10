using Insttantt.FieldsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Insttantt.FieldsManagement.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>(entity =>
            {
                entity.ToTable("Fields"); // Nombre de la tabla en la base de datos

                entity.HasKey(e => e.FieldId); // Clave primaria

                entity.Property(e => e.FieldId)
                    .HasColumnName("FieldId")
                    .IsRequired()
                    .ValueGeneratedOnAdd(); // Autoincremental

                entity.Property(e => e.FieldName)
                    .HasColumnName("FieldName")
                    .HasMaxLength(200)
                    .IsRequired(); // Nombre del campo

                entity.Property(e => e.FieldType)
                    .HasColumnName("FieldType")
                    .HasMaxLength(50)
                    .IsRequired(); // Tipo de datos del campo

                entity.Property(e => e.FieldRequired)
                    .HasColumnName("FieldRequired")
                    .HasColumnType("bit")
                    .IsRequired(); // Requerido o no requerido

                entity.Property(e => e.FieldValidation)
                    .HasColumnName("FieldValidation")
                    .HasMaxLength(500); // Expresión regular de validación del campo
            });
        }
    }
}
