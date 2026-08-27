using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace TheImmutableMed.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        public string Disease { get; set; } = string.Empty;

        [Range(0, 100)]
        public int OxygenLevel { get; set; }

        public int HeartRate { get; set; }

        public int BloodPressure { get; set; }

        public string MedicalHistory { get; set; } = string.Empty;

        public int? DoctorId { get; set; }

        public Doctor? Doctor { get; set; }
    }
}
