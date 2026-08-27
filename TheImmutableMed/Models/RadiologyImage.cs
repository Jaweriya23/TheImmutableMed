using Microsoft.AspNetCore.Http;

namespace TheImmutableMed.Models
{
    public class RadiologyImage
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        // final saved file path
        public string ImagePath { get; set; } = string.Empty;

        // 👇 for file upload from PC (NOT stored in DB)
        public IFormFile? ImageFile { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;

        public string AnalysisResult { get; set; } = string.Empty;
    }
}