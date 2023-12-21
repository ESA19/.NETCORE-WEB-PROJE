using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deneme.Data;
using Deneme.Models;
using Deneme.Areas.Identity.Data;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Deneme.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly DenemeDbContext _context;

       

        public AppointmentController(DenemeDbContext context)
        {
            _context = context;
            
          
        }
       
       
        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var denemeDbContext = _context.Appointments.Include(a => a.Department).Include(a => a.Doctor).Include(a => a.User).Where(a => a.User.Id == UserId);

            return View(await denemeDbContext.ToListAsync());


        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewBag.DepartmentName = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewBag.DoctorName = new SelectList(_context.Doctors, "DoctorId", "DoctorName");
            ViewBag.UserName = new SelectList(_context.Users, "Id", "FirstAndLastName");
            

            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,UserId,DoctorId,DepartmentId,AppointmentDate")] Appointments appointment)
        {
            bool hataVar = false;
            var appointmentDate = _context.Appointments.Where(x => x.AppointmentDate == appointment.AppointmentDate).Count();
            if (appointmentDate!=0)
            {
                
                
                    ViewBag.Mesaj = "Hata doktorun bu saate başka bir  randevusu var!";
                    hataVar = true;
                   
                
            }
            if (appointment.AppointmentDate.Hour < 09.00 || appointment.AppointmentDate.Hour > 17.00 )
            {
              
                    ViewBag.Mesaj = "9.00 17.00 arası olmalı";
                    hataVar = true;
                   
                
            }
            if (appointment.AppointmentDate.DayOfWeek==DayOfWeek.Sunday||appointment.AppointmentDate.DayOfWeek==DayOfWeek.Saturday)
            {
                ViewBag.Mesaj = "Hafta İçi Randevu Alınız!";
                hataVar = true;
            }

            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Mesaj = "ters gitti";
            //    hataVar = true;
            //}
            if (appointment.AppointmentDate.Date < DateTime.Today)
            {
                ViewBag.Mesaj = "Geçmiş Zamana Randevu alınmaz!";
                hataVar = true;
            }
            if (hataVar)
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", appointment.DepartmentId);
                ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", appointment.DoctorId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", appointment.UserId);
                ViewBag.DepartmentName = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", appointment.DepartmentId);
                ViewBag.DoctorName = new SelectList(_context.Doctors, "DoctorId", "DoctorName", appointment.DoctorId);
                ViewBag.UserName = new SelectList(_context.Users, "Id", "FirstAndLastName", appointment.UserId);
                
                return View(appointment);
            }
       

            _context.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         
        }

        // GET: Appointment/Edit/5
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", appointment.UserId);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,UserId,DoctorId,DepartmentId,AppointmentDate")] Appointments appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid||true)
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointment/Delete/5
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
