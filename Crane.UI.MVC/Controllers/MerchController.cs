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
    public class MerchController : Controller
    {
        private readonly craneContext _context;

        public MerchController(craneContext context)
        {
            _context = context;
        }

        // GET: Merch
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var craneContext = _context.Merches.Include(m => m.MerchType);
            return View(await craneContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Merch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Merches == null)
            {
                return NotFound();
            }

            var merch = await _context.Merches
                .Include(m => m.MerchType)
                .FirstOrDefaultAsync(m => m.MerchId == id);
            if (merch == null)
            {
                return NotFound();
            }

            return View(merch);
        }

        // GET: Merch/Create
        public IActionResult Create()
        {
            ViewData["MerchTypeId"] = new SelectList(_context.MerchTypes, "MerchTypeId", "MerchName");
            return View();
        }

        // POST: Merch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MerchId,MerchName,MerchDescription,MerchPrice,IsInStock,MerchTypeId,MerchImage")] Merch merch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MerchTypeId"] = new SelectList(_context.MerchTypes, "MerchTypeId", "MerchName", merch.MerchTypeId);
            return View(merch);
        }

        // GET: Merch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Merches == null)
            {
                return NotFound();
            }

            var merch = await _context.Merches.FindAsync(id);
            if (merch == null)
            {
                return NotFound();
            }
            ViewData["MerchTypeId"] = new SelectList(_context.MerchTypes, "MerchTypeId", "MerchName", merch.MerchTypeId);
            return View(merch);
        }

        // POST: Merch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MerchId,MerchName,MerchDescription,MerchPrice,IsInStock,MerchTypeId,MerchImage")] Merch merch)
        {
            if (id != merch.MerchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchExists(merch.MerchId))
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
            ViewData["MerchTypeId"] = new SelectList(_context.MerchTypes, "MerchTypeId", "MerchName", merch.MerchTypeId);
            return View(merch);
        }

        // GET: Merch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Merches == null)
            {
                return NotFound();
            }

            var merch = await _context.Merches
                .Include(m => m.MerchType)
                .FirstOrDefaultAsync(m => m.MerchId == id);
            if (merch == null)
            {
                return NotFound();
            }

            return View(merch);
        }

        // POST: Merch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Merches == null)
            {
                return Problem("Entity set 'craneContext.Merches'  is null.");
            }
            var merch = await _context.Merches.FindAsync(id);
            if (merch != null)
            {
                _context.Merches.Remove(merch);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchExists(int id)
        {
          return (_context.Merches?.Any(e => e.MerchId == id)).GetValueOrDefault();
        }
    }
}
