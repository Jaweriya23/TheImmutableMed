using TheImmutableMed.Models;

namespace TheImmutableMed.Data
{
    public static class InMemoryStore
    {
        public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Patient> Patients = new List<Patient>();
        public static List<RadiologyImage> RadiologyImages = new List<RadiologyImage>();
    }
}