using Estrrado_SchoolManagementSystem.Models;
using Estrrado_SchoolManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace Estrrado_SchoolManagementSystem.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly RegisterService _service;
        public RegistrationController(RegisterService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Looks for Views/Registration/Register.cshtml
        }

        [HttpPost]
        public IActionResult Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                int studentId = _service.RegristerUser(model);

                if (studentId <= 0)
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                    return View(model);
                }

                // Pass StudentID to success page
                ViewBag.StudentID = studentId;

                return View("RegisterSuccess", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

    }
}
