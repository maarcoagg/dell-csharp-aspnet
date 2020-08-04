using Microsoft.EntityFrameworkCore;

    public class HospitalContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Hospital");
        }

        public DbSet<Consultas> Consultas { get; set; }
        public DbSet<Enfermeiros> Enfermeiros { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Triagem> Triagem { get; set; }

    }