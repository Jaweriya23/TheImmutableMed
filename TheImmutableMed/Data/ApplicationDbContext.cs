using Microsoft.EntityFrameworkCore;
using TheImmutableMed.Models;

namespace TheImmutableMed.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<RadiologyImage> RadiologyImages { get; set; }
    }
}