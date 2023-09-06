using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crane.DATA.EF.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Crane.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BeerTypesController : Controller
    {
        private readonly craneContext _context;

        public BeerTypesController(craneContext context)
        {
            _context = context;
        }

        // GET: BeerTypes
        public async Task<IActionResult> Index()
        {
              return _context.BeerTypes != null ? 
                          View(await _context.BeerTypes.ToListAsync()) :
                          Problem("Entity set 'craneContext.BeerTypes'  is null.");
        }

        // GET: BeerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BeerTypes == null)
            {
                return NotFound();
            }

            var beerType = await _context.BeerTypes
                .FirstOrDefaultAsync(m => m.BeerTypeId == id);
            if (beerType == null)
            {
                return NotFound();
            }

            return View(beerType);
        }

        // GET: BeerTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BeerTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BeerTypeId,BeerTypeName,BeerTypeDescription")] BeerType beerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beerType);
        }

        // GET: BeerTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BeerTypes == null)
            {
                return NotFound();
            }

            var beerType = await _context.BeerTypes.FindAsync(id);
            if (beerType == null)
            {
                return NotFound();
            }
            return View(beerType);
        }

        // POST: BeerTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BeerTypeId,BeerTypeName,BeerTypeDescription")] BeerType beerType)
        {
            if (id != beerType.BeerTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerTypeExists(beerType.BeerTypeId))
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
            return View(beerType);
        }

        // GET: BeerTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BeerTypes == null)
            {
                return NotFound();
            }

            var beerType = await _context.BeerTypes
                .FirstOrDefaultAsync(m => m.BeerTypeId == id);
            if (beerType == null)
            {
                return NotFound();
            }

            return View(beerType);
        }

        // POST: BeerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BeerTypes == null)
            {
                return Problem("Entity set 'craneContext.BeerTypes'  is null.");
            }
            var beerType = await _context.BeerTypes.FindAsync(id);
            if (beerType != null)
            {
                _context.BeerTypes.Remove(beerType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerTypeExists(int id)
        {
          return (_context.BeerTypes?.Any(e => e.BeerTypeId == id)).GetValueOrDefault();
        }
    }
}
