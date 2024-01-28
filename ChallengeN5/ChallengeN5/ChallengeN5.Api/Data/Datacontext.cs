using ChallengeN5.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ChallengeN5.Api.Data
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) : base(options)
        {
        }

        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<TiposPermisos> TiposPermisos { get; set; }
        public DbSet<Permisos> Permisos { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
        base.OnModelCreating(modelBuilder);

             // Configura la relación entre EmpleadosTipoPermisos y Empleados
             modelBuilder.Entity<Permisos>()
            .HasOne(etp => etp.Empleado)
            .WithMany(e => e.Permisos) 
            .HasForeignKey(etp => etp.EmpleadoID)
            .IsRequired(); 

             // Configura la relación entre EmpleadosTipoPermisos y TiposPermisos
             modelBuilder.Entity<Permisos>()
            .HasOne(etp => etp.TipoPermiso)
            .WithMany(tp => tp.Permisos) 
            .HasForeignKey(etp => etp.TipoPermisoID)
            .IsRequired();

             // Crear índice único en el campo Documento de la tabla Empleados
             modelBuilder.Entity<Empleados>()
            .HasIndex(e => e.EmepleadoDocidentidad)
            .IsUnique();

             // Crear índice único en el campo Descripcion de la tabla TiposPermisos
             modelBuilder.Entity<TiposPermisos>()
            .HasIndex(tp => tp.TipoPermisoDescripcion)
            .IsUnique();
        }
    }
}
