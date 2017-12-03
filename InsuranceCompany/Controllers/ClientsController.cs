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
    public class ClientsController : Controller
    {
        private readonly ApplicationContext _context;

        public ClientsController(ApplicationContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,user")]
        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Clients.Include(c => c.Group);
            return View(await applicationContext.ToListAsync());
        }
        [Authorize(Roles = "admin,user")]
        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }
        [Authorize(Roles = "admin")]
        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "Id");
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientName,ClientDateBirth,ClientSex,ClientAddress,ClientPhone,ClientPassport,GroupId")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", client.GroupId);
            return View(client);
        }
        [Authorize(Roles = "admin")]
        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", client.GroupId);
            return View(client);
        }
        [Authorize(Roles = "admin")]
        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientName,ClientDateBirth,ClientSex,ClientAddress,ClientPhone,ClientPassport,GroupId")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", client.GroupId);
            return View(client);
        }
        [Authorize(Roles = "admin")]
        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }
        [Authorize(Roles = "admin")]
        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
