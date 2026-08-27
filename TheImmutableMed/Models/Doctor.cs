using System.ComponentModel.DataAnnotations;

namespace TheImmutableMed.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public ICollection<Patient>? Patients { get; set; }
    }
}