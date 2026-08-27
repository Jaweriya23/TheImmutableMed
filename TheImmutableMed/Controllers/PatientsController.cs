using Microsoft.AspNetCore.Mvc;
using TheImmutableMed.Models;
using TheImmutableMed.Services;

namespace TheImmutableMed.Controllers
{
    public class PatientsController : Controller
    {
        private readonly TriageService _service;

        public PatientsController(TriageService service)
        {
            _service = service;
        }

        // INDEX
        public IActionResult Index()
        {
            return View(_service.GetPatients());
        }

        // CREATE
        public IActionResult Create()
        {
            ViewBag.Doctors = _service.GetDoctors();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = _service.GetDoctors();
                return View(patient);
            }

            _service.AddPatient(patient);
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var patient = _service.GetPatients().FirstOrDefault(p => p.Id == id);

            if (patient == null) return NotFound();

            return View(patient);
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var patient = _service.GetPatients().FirstOrDefault(p => p.Id == id);

            if (patient == null) return NotFound();

            ViewBag.Doctors = _service.GetDoctors();
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient updated)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = _service.GetDoctors();
                return View(updated);
            }

            _service.UpdatePatient(updated);

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var patient = _service.GetPatients().FirstOrDefault(p => p.Id == id);

            if (patient != null)
            {
                _service.GetPatients().Remove(patient);
            }

            return RedirectToAction(nameof(Index));
        }

        // CRITICAL
        public IActionResult Critical()
        {
            return View(_service.GetCriticalPatients());
        }
    }
}