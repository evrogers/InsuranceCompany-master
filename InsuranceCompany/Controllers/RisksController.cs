using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsuranceCompany.Models;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceCompany.Controllers
{
    public class RisksController : Controller
    {
        private readonly ApplicationContext _context;

        public RisksController(ApplicationContext context)
        {
            _context = context;
        }



        [Authorize(Roles = "admin,user")]
        public IActionResult Index(string RiskName, string RiskDescription, int page = 1, SortState sortOrder = SortState.RiskNameAsc)
        {
            int pageSize = 10;

            IQueryable<Risks> source = _context.Risks.Include(r => r.Type);



            ViewData["RiskNameSort"] = sortOrder == SortState.RiskNameAsc ? SortState.RiskNameDesc : SortState.RiskNameAsc;
            ViewData["RiskDescriptionSort"] = sortOrder == SortState.RiskDescriptionAsc ? SortState.RiskDescriptionDesc : SortState.RiskDescriptionAsc;
      

            if (RiskName != null)
            {
                source = source.Where(x => x.RiskName == RiskName);
            }
            if (RiskDescription != null)
            {
                source = source.Where(x => x.RiskDescription == RiskDescription);
            }
   

            switch (sortOrder)
            {
                case SortState.RiskNameAsc:
                    source = source.OrderBy(x => x.RiskName);
                    break;
                case SortState.RiskNameDesc:
                    source = source.OrderByDescending(x => x.RiskName);
                    break;
                case SortState.RiskDescriptionAsc:
                    source = source.OrderBy(x => x.RiskDescription);
                    break;
                case SortState.RiskDescriptionDesc:
                    source = source.OrderByDescending(x => x.RiskDescription);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);

            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                FilterRisksViewModel = new FilterRisksViewModel(RiskName,RiskDescription),
                Risks = items
            };
            return View(ivm);

        }
        [Authorize(Roles = "admin,user")]
        // GET: Risks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var risks = await _context.Risks
                .Include(r => r.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (risks == null)
            {
                return NotFound();
            }

            return View(risks);
        }
        [Authorize(Roles = "admin")]
        // GET: Risks/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "Id");
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Risks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RiskName,RiskDescription,AverageProbability,TypeId")] Risks risks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(risks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "Id", risks.TypeId);
            return View(risks);
        }
        [Authorize(Roles = "admin")]
        // GET: Risks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var risks = await _context.Risks.SingleOrDefaultAsync(m => m.Id == id);
            if (risks == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "Id", risks.TypeId);
            return View(risks);
        }
        [Authorize(Roles = "admin")]
        // POST: Risks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RiskName,RiskDescription,AverageProbability,TypeId")] Risks risks)
        {
            if (id != risks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(risks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RisksExists(risks.Id))
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
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "Id", risks.TypeId);
            return View(risks);
        }
        [Authorize(Roles = "admin")]
        // GET: Risks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var risks = await _context.Risks
                .Include(r => r.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (risks == null)
            {
                return NotFound();
            }

            return View(risks);
        }
        [Authorize(Roles = "admin")]
        // POST: Risks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var risks = await _context.Risks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Risks.Remove(risks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RisksExists(int id)
        {
            return _context.Risks.Any(e => e.Id == id);
        }
    }
}
