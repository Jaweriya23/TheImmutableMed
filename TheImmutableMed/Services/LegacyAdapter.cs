using Microsoft.Extensions.Caching.Memory;

namespace TheImmutableMed.Services
{
    public class LegacyAdapter
    {
        private readonly IMemoryCache _cache;

        private readonly LegacyHospitalDatabase _legacyDb;

        public LegacyAdapter(IMemoryCache cache)
        {
            _cache = cache;
            _legacyDb = new LegacyHospitalDatabase();
        }

        public string FetchPatientHistory(int patientId)
        {
            if (!_cache.TryGetValue(patientId, out string? history))
            {
                history = _legacyDb.GetPatientRecord(patientId);

                _cache.Set(patientId, history,
                    TimeSpan.FromMinutes(10));
            }

            return history ?? "No History Found";
        }
    }
}