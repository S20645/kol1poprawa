using kol1poprawa.Models;
using kol1poprawa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kol1poprawa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetPrescriptions()
        {
            return Ok(_service.GetPrescriptions());
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorAsync(Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest("Złe dane");
            if (_service.IsDoctorIDExists(doctor.IdDoctor))
                return BadRequest("Podane ID jest zajete.");

            return Ok(_service.AddDoctor(doctor));
        }
    }
}
