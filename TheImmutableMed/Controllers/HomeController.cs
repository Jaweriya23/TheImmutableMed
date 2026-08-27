using Microsoft.AspNetCore.Mvc;
using TheImmutableMed.Services;

namespace TheImmutableMed.Controllers
{
    public class HomeController : Controller
    {
        private readonly TriageService _triage;

        public HomeController(TriageService triage)
        {
            _triage = triage;
        }

        public IActionResult Index()
        {
            ViewBag.TotalPatients = _triage.GetPatients().Count;
            ViewBag.CriticalPatients = _triage.GetCriticalPatients().Count;
            ViewBag.TotalDoctors = 0; // no DB version

            return View();
        }
    }
}

