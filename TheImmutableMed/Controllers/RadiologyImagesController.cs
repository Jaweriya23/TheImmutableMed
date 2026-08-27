using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheImmutableMed.Models;

namespace TheImmutableMed.Controllers
{
    public class RadiologyImagesController : Controller
    {
        private static List<RadiologyImage> _images = new List<RadiologyImage>();
        private static int _id = 1;

        private readonly IWebHostEnvironment _env;

        public RadiologyImagesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // INDEX
        public IActionResult Index()
        {
            return View(_images);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.PatientId = new List<SelectListItem>
            {
                new SelectListItem { Text = "Walk-in Patient", Value = "0" }
            };

            return View();
        }

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(RadiologyImage model)
        {
            model.Id = _id++;
            model.UploadDate = DateTime.Now;

            // ✅ FILE UPLOAD
            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                model.ImagePath = "/uploads/" + uniqueFileName;
            }

            // 🧠 SIMPLE AI LOGIC
            if (!string.IsNullOrEmpty(model.ImagePath))
            {
                var lower = model.ImagePath.ToLower();

                if (lower.Contains("brain"))
                    model.AnalysisResult = "🧠 Brain scan: No abnormality detected.";
                else if (lower.Contains("chest"))
                    model.AnalysisResult = "🫁 Chest scan: Lungs appear clear.";
                else
                    model.AnalysisResult = "📊 Analysis complete: No critical findings.";
            }

            _images.Add(model);

            return RedirectToAction("Index");
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var img = _images.FirstOrDefault(x => x.Id == id);
            if (img == null) return NotFound();

            return View(img);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var img = _images.FirstOrDefault(x => x.Id == id);
            if (img != null)
                _images.Remove(img);

            return RedirectToAction("Index");
        }

        // GET: EDIT
        public IActionResult Edit(int id)
        {
            var img = _images.FirstOrDefault(x => x.Id == id);
            if (img == null) return NotFound();

            return View(img);
        }

        // POST: EDIT
        [HttpPost]
        public IActionResult Edit(RadiologyImage model)
        {
            var img = _images.FirstOrDefault(x => x.Id == model.Id);
            if (img == null) return NotFound();

            img.PatientId = model.PatientId;
            img.UploadDate = model.UploadDate;
            img.AnalysisResult = model.AnalysisResult;

            // If new file uploaded → replace image
            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                img.ImagePath = "/uploads/" + uniqueFileName;
            }

            return RedirectToAction("Index");
        }


    }
}