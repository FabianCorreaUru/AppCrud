using Microsoft.EntityFrameworkCore;
using AppCrudFabian.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppCrudFabian.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {
        
        }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Certificacion> Certificaciones { get; set; }

        public DbSet<EmpleadosCertificaciones> EmpleadosCertificaciones { get; set; }

        public DbSet<Ciudad> Ciudades { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>(tb =>
            {
                tb.HasKey(col => col.IdEmpleado);
                tb.Property(col => col.IdEmpleado).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.NombreCompleto).HasMaxLength(50);
                tb.Property(col => col.Correo).HasMaxLength(50);
            });

            modelBuilder.Entity<Empleado>().ToTable("Empleado");

            modelBuilder.Entity<Area>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Area>().ToTable("Area");

            modelBuilder.Entity<Certificacion>(tb =>
            {
                tb.HasKey(col => col.IdCertificacion);
                tb.Property(col => col.IdCertificacion).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).HasMaxLength(120);
                tb.Property(col => col.Descripcion);                
            });

            modelBuilder.Entity<Certificacion>().ToTable("Certificacion");

            // Definimos la llave primaria

            modelBuilder.Entity<EmpleadosCertificaciones>().HasKey(ec => new { ec.EmpleadoId, ec.CertificacionId } );

            // definimos nuestras relaciones entre tablas o ForeignKey

            // Certificaciones
            modelBuilder.Entity<EmpleadosCertificaciones>()
                .HasOne<Certificacion>(ec => ec.Certificacion)
                .WithMany(e => e.EmpleadosCertificaciones)
                .HasForeignKey(ec => ec.CertificacionId);

            // Empleados
            modelBuilder.Entity<EmpleadosCertificaciones>()
                .HasOne<Empleado>(ec => ec.Empleado)
                .WithMany(e => e.EmpleadosCertificaciones)
                .HasForeignKey(ec => ec.EmpleadoId);

            modelBuilder.Entity<Ciudad>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Ciudad>().ToTable("Ciudad");

            modelBuilder.Entity<Departamento>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Departamento>().ToTable("Departamento");

        }

    }
}
