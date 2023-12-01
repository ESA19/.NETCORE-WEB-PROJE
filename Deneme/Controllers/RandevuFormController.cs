using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deneme.Data;
using Deneme.Models;

namespace Deneme.Controllers
{
    public class RandevuFormController : Controller
    {
        private readonly DenemeDbContext _context;

        public RandevuFormController(DenemeDbContext context)
        {
            _context = context;
        }

        // GET: RandevuForm
        public async Task<IActionResult> Index()
        {
            var denemeDbContext = _context.randevusforms.Include(r => r.department).Include(r => r.doctor);
            return View(await denemeDbContext.ToListAsync());
        }

        // GET: RandevuForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.randevusforms == null)
            {
                return NotFound();
            }

            var randevuForm = await _context.randevusforms
                .Include(r => r.department)
                .Include(r => r.doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevuForm == null)
            {
                return NotFound();
            }

            return View(randevuForm);
        }

        // GET: RandevuForm/Create
        public IActionResult Create()
        {
            ViewData["departmentId"] = new SelectList(_context.departments, "Id", "Id");
            ViewData["doctorId"] = new SelectList(_context.doctors, "Id", "Id");
            return View();
        }

        // POST: RandevuForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,doctorId,departmentId,appointmentdateTime")] RandevuForm randevuForm)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(randevuForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["departmentId"] = new SelectList(_context.departments, "Id", "Id", randevuForm.departmentId);
            ViewData["doctorId"] = new SelectList(_context.doctors, "Id", "Id", randevuForm.doctorId);
            return View(randevuForm);
        }

        // GET: RandevuForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.randevusforms == null)
            {
                return NotFound();
            }

            var randevuForm = await _context.randevusforms.FindAsync(id);
            if (randevuForm == null)
            {
                return NotFound();
            }
            ViewData["departmentId"] = new SelectList(_context.departments, "Id", "Id", randevuForm.departmentId);
            ViewData["doctorId"] = new SelectList(_context.doctors, "Id", "Id", randevuForm.doctorId);
            return View(randevuForm);
        }

        // POST: RandevuForm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,doctorId,departmentId,appointmentdateTime")] RandevuForm randevuForm)
        {
            if (id != randevuForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid|| true)
            {
                try
                {
                    _context.Update(randevuForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuFormExists(randevuForm.Id))
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
            ViewData["departmentId"] = new SelectList(_context.departments, "Id", "Id", randevuForm.departmentId);
            ViewData["doctorId"] = new SelectList(_context.doctors, "Id", "Id", randevuForm.doctorId);
            return View(randevuForm);
        }

        // GET: RandevuForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.randevusforms == null)
            {
                return NotFound();
            }

            var randevuForm = await _context.randevusforms
                .Include(r => r.department)
                .Include(r => r.doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevuForm == null)
            {
                return NotFound();
            }

            return View(randevuForm);
        }

        // POST: RandevuForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.randevusforms == null)
            {
                return Problem("Entity set 'DenemeDbContext.randevusforms'  is null.");
            }
            var randevuForm = await _context.randevusforms.FindAsync(id);
            if (randevuForm != null)
            {
                _context.randevusforms.Remove(randevuForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuFormExists(int id)
        {
          return (_context.randevusforms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
