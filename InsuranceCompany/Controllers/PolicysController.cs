using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsuranceCompany.Models;
using InsuranceCompany.ViewModels.FilterModel;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceCompany.Controllers
{
    public class PolicysController : Controller
    {
        private readonly ApplicationContext _context;

        public PolicysController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin,user")]
        public IActionResult Index(int? PolicyNumber, int? Summ, int page = 1, SortState sortOrder = SortState.PolicyNameAsc)
        {
            int pageSize = 10;

            IQueryable<Policys> source = _context.Policys.Include(p => p.Client).Include(p => p.Staff).Include(p => p.Type);
        


            ViewData["PolicyNumberSort"] = sortOrder == SortState.PolicyNumberAsc ? SortState.PolicyNumberDesc : SortState.PolicyNumberAsc;
            ViewData["SummSort"] = sortOrder == SortState.SummAsc ? SortState.SummDesc: SortState.SummAsc;


            if (PolicyNumber != null)
            {
                source = source.Where(x => x.PolicyNumber == PolicyNumber);
            }
            if (Summ != null)
            {
                source = source.Where(x => x.Summ == Summ);
            }


            switch (sortOrder)
            {
                case SortState.PolicyNumberAsc:
                    source = source.OrderBy(x => x.PolicyNumber);
                    break;
                case SortState.PolicyNumberDesc:
                    source = source.OrderByDescending(x => x.PolicyNumber);
                    break;
                case SortState.SummAsc:
                    source = source.OrderBy(x => x.Summ);
                    break;
                case SortState.SummDesc:
                    source = source.OrderByDescending(x => x.Summ);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);

            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                FilterPolicysViewModel = new FilterPolicysViewModel(PolicyNumber,Summ),
                Policys = items
            };
            return View(ivm);

        }





        [Authorize(Roles = "admin,user")]

        // GET: Policys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policys = await _context.Policys
                .Include(p => p.Client)
                .Include(p => p.Staff)
                .Include(p => p.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (policys == null)
            {
                return NotFound();
            }

            return View(policys);
        }
        [Authorize(Roles = "admin")]
        // GET: Policys/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "ClientName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "PolicyName");
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Policys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PolicyNumber,DateBegin,DateEnd,Cost,Summ,TypeId,PaymentMark,EndMark,ClientId,StaffId")] Policys policys)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policys);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "ClientName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "PolicyName");
            return View(policys);
        }
        [Authorize(Roles = "admin")]
        // GET: Policys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policys = await _context.Policys.SingleOrDefaultAsync(m => m.Id == id);
            if (policys == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "ClientName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "PolicyName");
            return View(policys);
        }
        [Authorize(Roles = "admin")]
        // POST: Policys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PolicyNumber,DateBegin,DateEnd,Cost,Summ,TypeId,PaymentMark,EndMark,ClientId,StaffId")] Policys policys)
        {
            if (id != policys.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policys);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicysExists(policys.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "ClientName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            ViewData["TypeId"] = new SelectList(_context.PolicyTypes, "Id", "PolicyName");
            return View(policys);
        }
        [Authorize(Roles = "admin")]
        // GET: Policys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policys = await _context.Policys
                .Include(p => p.Client)
                .Include(p => p.Staff)
                .Include(p => p.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (policys == null)
            {
                return NotFound();
            }

            return View(policys);
        }
        [Authorize(Roles = "admin")]
        // POST: Policys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policys = await _context.Policys.SingleOrDefaultAsync(m => m.Id == id);
            _context.Policys.Remove(policys);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicysExists(int id)
        {
            return _context.Policys.Any(e => e.Id == id);
        }
    }
}
