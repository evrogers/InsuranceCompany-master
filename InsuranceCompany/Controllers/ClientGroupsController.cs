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
    public class ClientGroupsController : Controller
    {
        private readonly ApplicationContext _context;

        public ClientGroupsController(ApplicationContext context)
        {
            _context = context;
        }

		        [Authorize(Roles = "admin,user")]
        // GET: ClientGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientGroups.ToListAsync());
        }
        [Authorize(Roles = "admin,user")]
        // GET: ClientGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroups = await _context.ClientGroups
                .SingleOrDefaultAsync(m => m.Id == id);
            if (clientGroups == null)
            {
                return NotFound();
            }

            return View(clientGroups);
        }
        [Authorize(Roles = "admin")]
        // GET: ClientGroups/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: ClientGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupName,GroupDescription")] ClientGroups clientGroups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientGroups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientGroups);
        }
        [Authorize(Roles = "admin")]
        // GET: ClientGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroups = await _context.ClientGroups.SingleOrDefaultAsync(m => m.Id == id);
            if (clientGroups == null)
            {
                return NotFound();
            }
            return View(clientGroups);
        }
        [Authorize(Roles = "admin")]
        // POST: ClientGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,GroupDescription")] ClientGroups clientGroups)
        {
            if (id != clientGroups.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientGroups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientGroupsExists(clientGroups.Id))
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
            return View(clientGroups);
        }
        [Authorize(Roles = "admin")]
        // GET: ClientGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroups = await _context.ClientGroups
                .SingleOrDefaultAsync(m => m.Id == id);
            if (clientGroups == null)
            {
                return NotFound();
            }

            return View(clientGroups);
        }
        [Authorize(Roles = "admin")]
        // POST: ClientGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientGroups = await _context.ClientGroups.SingleOrDefaultAsync(m => m.Id == id);
            _context.ClientGroups.Remove(clientGroups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientGroupsExists(int id)
        {
            return _context.ClientGroups.Any(e => e.Id == id);
        }
    }
}
