using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Context;
using WebApplication4.DTO;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : Controller
    {
        private readonly MedDBContext _context;

        public PrescriptionsController(MedDBContext context)
        {
            _context = context;
        }

        // GET: Prescriptions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var medDBContext = _context.Prescriptions.Include(p => p.Doctor).Include(p => p.Patient);
            return View(await medDBContext.ToListAsync());
        }

        // GET: Prescriptions/Details/5
        [HttpGet("{id}")]
        public IActionResult GetPatient(int id)
        {

            var patient = _context.Patients
                .Include(p => p.Prescriptions.OrderBy(p => p.DueDate).ThenBy(p => p.Date))
                    .ThenInclude(p => p.Doctor)
                .Include(p => p.Prescriptions)
                    .ThenInclude(p => p.Prescription_Medicaments)
                        .ThenInclude(pm => pm.Medicament)
                .FirstOrDefault(p => p.IdPatient == id);

            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            return Ok(patient);
        }


        [HttpPost("Create")]
        public IActionResult PostPrescription([FromBody] PrescriptionRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var patient = _context.Patients.FirstOrDefault(p => p.IdPatient == request.IdPatient);
            if (patient == null)
            {

                patient = new Patient
                {
                    IdPatient = request.IdPatient,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Birthdate = request.Birthdate
                };

                _context.Patients.Add(patient);
            }

            if (request.Medicaments.Count > 10)
            {
                return BadRequest("A prescription can include a maximum of 10 medications");
            }

            if (request.DueDate < request.Date)
            {
                return BadRequest("Due date must be greater than or equal to the date");
            }

            foreach (var medication in request.Medicaments)
            {
                var existingMedication = _context.Medicaments.FirstOrDefault(m => m.IdMedicament == medication.IdMedicament);
                if (existingMedication == null)
                {

                    return NotFound($"Medication with ID {medication.IdMedicament} does not exist");
                }
            }

            var doctor = _context.Doctors.FirstOrDefault(d => d.IdDoctor == request.IdDoctor);
            if (doctor == null)
            {
                return NotFound("Doctor does not exist");

            }

            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                IdDoctor = request.IdDoctor,
                IdPatient = request.IdPatient,
                Patient = patient,
                Doctor = doctor
            };

            _context.Prescriptions.Add(prescription);
            _context.SaveChanges();

            return Ok("Prescription issued successfully");
        }
    }
}
