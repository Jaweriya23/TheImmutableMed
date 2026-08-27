using TheImmutableMed.Data;
using TheImmutableMed.Models;

namespace TheImmutableMed.Services
{
    public class TriageService
    {
        // ✅ USE SHARED MEMORY
        private static List<Patient> patients = InMemoryStore.Patients;

        // ---------------- DOCTORS ----------------

        public List<Doctor> GetDoctors()
        {
            return InMemoryStore.Doctors;
        }

        public void AddDoctor(Doctor doctor)
        {
            doctor.Id = InMemoryStore.Doctors.Count + 1;

            InMemoryStore.Doctors.Add(doctor);
        }

        // ---------------- PATIENTS ----------------

        public List<Patient> GetPatients()
        {
            foreach (var p in patients)
            {
                p.Doctor = InMemoryStore.Doctors
                    .FirstOrDefault(d => d.Id == p.DoctorId);
            }

            return patients;
        }

        public void AddPatient(Patient patient)
        {
            patient.Id = patients.Count + 1;

            patient.Doctor = InMemoryStore.Doctors
                .FirstOrDefault(d => d.Id == patient.DoctorId);

            patients.Add(patient);
        }

        public void UpdatePatient(Patient updated)
        {
            var patient = patients
                .FirstOrDefault(p => p.Id == updated.Id);

            if (patient == null)
                return;

            patient.Name = updated.Name;
            patient.Age = updated.Age;
            patient.Gender = updated.Gender;
            patient.Disease = updated.Disease;
            patient.OxygenLevel = updated.OxygenLevel;
            patient.HeartRate = updated.HeartRate;
            patient.BloodPressure = updated.BloodPressure;
            patient.MedicalHistory = updated.MedicalHistory;

            patient.DoctorId = updated.DoctorId;

            // ✅ UPDATE LINKED DOCTOR
            patient.Doctor = InMemoryStore.Doctors
                .FirstOrDefault(d => d.Id == updated.DoctorId);
        }

        // ---------------- CRITICAL ----------------

        public List<Patient> GetCriticalPatients()
        {
            return GetPatients().Where(p =>
                p.OxygenLevel <= 88 || 
                p.HeartRate >= 130 ||
                p.HeartRate <= 40 ||
                p.BloodPressure > 140 ||
                (p.BloodPressure >= 40 &&
                 p.BloodPressure <= 60)
            ).ToList();
        }
    }
}