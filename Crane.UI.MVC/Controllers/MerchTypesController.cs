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
    public class MerchTypesController : Controller
    {
        private readonly craneContext _context;

        public MerchTypesController(craneContext context)
        {
            _context = context;
        }

        // GET: MerchTypes
        public async Task<IActionResult> Index()
        {
              return _context.MerchTypes != null ? 
                          View(await _context.MerchTypes.ToListAsync()) :
                          Problem("Entity set 'craneContext.MerchTypes'  is null.");
        }

        // GET: MerchTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MerchTypes == null)
            {
                return NotFound();
            }

            var merchType = await _context.MerchTypes
                .FirstOrDefaultAsync(m => m.MerchTypeId == id);
            if (merchType == null)
            {
                return NotFound();
            }

            return View(merchType);
        }

        // GET: MerchTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MerchTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MerchTypeId,MerchName,MerchDescription")] MerchType merchType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merchType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(merchType);
        }

        // GET: MerchTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MerchTypes == null)
            {
                return NotFound();
            }

            var merchType = await _context.MerchTypes.FindAsync(id);
            if (merchType == null)
            {
                return NotFound();
            }
            return View(merchType);
        }

        // POST: MerchTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MerchTypeId,MerchName,MerchDescription")] MerchType merchType)
        {
            if (id != merchType.MerchTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merchType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchTypeExists(merchType.MerchTypeId))
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
            return View(merchType);
        }

        // GET: MerchTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MerchTypes == null)
            {
                return NotFound();
            }

            var merchType = await _context.MerchTypes
                .FirstOrDefaultAsync(m => m.MerchTypeId == id);
            if (merchType == null)
            {
                return NotFound();
            }

            return View(merchType);
        }

        // POST: MerchTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MerchTypes == null)
            {
                return Problem("Entity set 'craneContext.MerchTypes'  is null.");
            }
            var merchType = await _context.MerchTypes.FindAsync(id);
            if (merchType != null)
            {
                _context.MerchTypes.Remove(merchType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchTypeExists(int id)
        {
          return (_context.MerchTypes?.Any(e => e.MerchTypeId == id)).GetValueOrDefault();
        }
    }
}
