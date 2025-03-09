using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp3dotnet.Data;
using tp3dotnet.Models;

namespace tp3dotnet.Controllers
{
    public class MemberShipTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberShipTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberShipTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberShipType.ToListAsync());
        }

        // GET: MemberShipTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberShipType = await _context.MemberShipType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberShipType == null)
            {
                return NotFound();
            }

            return View(memberShipType);
        }

        // GET: MemberShipTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberShipTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SignUpFee,DurationInMonth,DiscountRate")] MemberShipType memberShipType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberShipType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberShipType);
        }

        // GET: MemberShipTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberShipType = await _context.MemberShipType.FindAsync(id);
            if (memberShipType == null)
            {
                return NotFound();
            }
            return View(memberShipType);
        }

        // POST: MemberShipTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SignUpFee,DurationInMonth,DiscountRate")] MemberShipType memberShipType)
        {
            if (id != memberShipType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberShipType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberShipTypeExists(memberShipType.Id))
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
            return View(memberShipType);
        }

        // GET: MemberShipTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberShipType = await _context.MemberShipType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberShipType == null)
            {
                return NotFound();
            }

            return View(memberShipType);
        }

        // POST: MemberShipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberShipType = await _context.MemberShipType.FindAsync(id);
            if (memberShipType != null)
            {
                _context.MemberShipType.Remove(memberShipType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberShipTypeExists(int id)
        {
            return _context.MemberShipType.Any(e => e.Id == id);
        }
    }
}
