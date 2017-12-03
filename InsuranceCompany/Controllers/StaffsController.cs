using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceCompany.Controllers
{
    public class StaffsController : Controller
    {
        private readonly ApplicationContext _context;

        public StaffsController(ApplicationContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "admin,user")]
        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staffs.ToListAsync());
        }
        [Authorize(Roles = "admin,user")]
        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }
        [Authorize(Roles = "admin")]
        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffName,StaffPost,StaffExperience")] Staffs staffs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffs);
        }
        [Authorize(Roles = "admin")]
        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.SingleOrDefaultAsync(m => m.Id == id);
            if (staffs == null)
            {
                return NotFound();
            }
            return View(staffs);
        }
        [Authorize(Roles = "admin")]
        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StaffName,StaffPost,StaffExperience")] Staffs staffs)
        {
            if (id != staffs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffsExists(staffs.Id))
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
            return View(staffs);
        }
        [Authorize(Roles = "admin")]
        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }
        [Authorize(Roles = "admin")]
        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffs = await _context.Staffs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Staffs.Remove(staffs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffsExists(int id)
        {
            return _context.Staffs.Any(e => e.Id == id);
        }
    }
}
