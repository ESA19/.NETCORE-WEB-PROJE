using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deneme.Data;
using Deneme.Models;
using Microsoft.AspNetCore.Identity;
using Deneme.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Deneme.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly DenemeDbContext _context;

        public AppointmentController(DenemeDbContext context)
        {
            _context = context;
        }

        

       
        public async Task<IActionResult> Index()
        {

            var denemeDbContext = _context.Appointments.Include(a => a.Department).Include(a => a.Doctor);
            return View(await denemeDbContext.ToListAsync());


        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            ViewBag.DepartmentName = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewBag.DoctorName = new SelectList(_context.Doctors, "DoctorId", "DoctorName");

            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,PatientName,DoctorId,DepartmentId,AppointmentDate")] Appointment appointment)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", appointment.DepartmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", appointment.DoctorId);
            return View(appointment);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", appointment.DepartmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", appointment.DoctorId);
            return View(appointment);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,PatientName,DoctorId,DepartmentId,AppointmentDate")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", appointment.DepartmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", appointment.DoctorId);
            return View(appointment);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'DenemeDbContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }

        public JsonResult GetDoctors(int departmentId)
        {
            return Json(_context.Doctors.Where(u => u.DepartmentId == departmentId).ToList());
        }

    }
}
