using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crane.DATA.EF.Models;
using Microsoft.AspNetCore.Authorization;

namespace Crane.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BeersController : Controller
    {
        private readonly craneContext _context;

        public BeersController(craneContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Beers
        public async Task<IActionResult> Index()
        {
            //var beers = _context.Beers.Where(b => !b.IsCurrent)
            //    .Include(b => b.BeerType);

            var craneContext = _context.Beers.Include(b => b.BeerType);
            return View(await craneContext.ToListAsync());
        }

        ////GET: Beers/TiledBeers
        //[AllowAnonymous]
        //public async Task<IActionResult> TiledBeers()
        //{
        //    //var beers = _context.Beers.Where(b => !b.IsCurrent)
        //    //    .Include(b => b.BeerType);

        //    var craneContext = _context.Beers.Include(b => b.BeerType);
        //    return View();
        //}

        [AllowAnonymous]
        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Beers == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .Include(b => b.BeerType)
                .FirstOrDefaultAsync(m => m.BeerId == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            ViewData["BeerTypeId"] = new SelectList(_context.BeerTypes, "BeerTypeId", "BeerTypeName");
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BeerId,BeerName,BeerDescription,BeerAbv,IsOnTap,BeerTypeId,BeerImage,IsCurrent")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeerTypeId"] = new SelectList(_context.BeerTypes, "BeerTypeId", "BeerTypeName", beer.BeerTypeId);
            return View(beer);
        }

        // GET: Beers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Beers == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            ViewData["BeerTypeId"] = new SelectList(_context.BeerTypes, "BeerTypeId", "BeerTypeName", beer.BeerTypeId);
            return View(beer);
        }

        // POST: Beers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BeerId,BeerName,BeerDescription,BeerAbv,IsOnTap,BeerTypeId,BeerImage,IsCurrent")] Beer beer)
        {
            if (id != beer.BeerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.BeerId))
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
            ViewData["BeerTypeId"] = new SelectList(_context.BeerTypes, "BeerTypeId", "BeerTypeName", beer.BeerTypeId);
            return View(beer);
        }

        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Beers == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .Include(b => b.BeerType)
                .FirstOrDefaultAsync(m => m.BeerId == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Beers == null)
            {
                return Problem("Entity set 'craneContext.Beers'  is null.");
            }
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int id)
        {
          return (_context.Beers?.Any(e => e.BeerId == id)).GetValueOrDefault();
        }
    }
}
