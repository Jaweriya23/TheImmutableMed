using Microsoft.AspNetCore.Mvc;
using TheImmutableMed.Data;
using TheImmutableMed.Models;

namespace TheImmutableMed.Controllers
{
    public class DoctorsController : Controller
    {
        // shared memory (IMPORTANT)
        private static int _id = 1;

        // GET: Doctors
        public IActionResult Index()
        {
            return View(InMemoryStore.Doctors);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            doctor.Id = _id++;
            InMemoryStore.Doctors.Add(doctor);

            return RedirectToAction(nameof(Index));
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var doctor = InMemoryStore.Doctors.FirstOrDefault(x => x.Id == id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor updatedDoctor)
        {
            var doctor = InMemoryStore.Doctors.FirstOrDefault(x => x.Id == updatedDoctor.Id);

            if (doctor == null)
                return NotFound();

            doctor.Name = updatedDoctor.Name;
            doctor.Specialization = updatedDoctor.Specialization;
            doctor.Contact = updatedDoctor.Contact;

            return RedirectToAction(nameof(Index));
        }

        // GET: Details
        public IActionResult Details(int id)
        {
            var doctor = InMemoryStore.Doctors.FirstOrDefault(x => x.Id == id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var doctor = InMemoryStore.Doctors.FirstOrDefault(x => x.Id == id);

            if (doctor != null)
            {
                InMemoryStore.Doctors.Remove(doctor);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}