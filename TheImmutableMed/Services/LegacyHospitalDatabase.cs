namespace TheImmutableMed.Services
{
    public class LegacyHospitalDatabase
    {
        public string GetPatientRecord(int id)
        {
            Thread.Sleep(5000);

            return "Legacy Patient History Loaded";
        }
    }
}

