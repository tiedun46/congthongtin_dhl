using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CongThongTinDHL.Models;

namespace CongThongTinDHL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuDeController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public ChuDeController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: Admin/ChuDe
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChuDes.ToListAsync());
        }

        // GET: Admin/ChuDe/Details/:id
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes
                .FirstOrDefaultAsync(m => m.MaChuDe == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // GET: Admin/ChuDe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChuDe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChuDe,TenCd,GhiChu,Status")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuDe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Edit/:id
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes.FindAsync(id);
            if (chuDe == null)
            {
                return NotFound();
            }
            return View(chuDe);
        }

        // POST: Admin/ChuDe/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaChuDe,TenCd,GhiChu,Status")] ChuDe chuDe)
        {
            if (id != chuDe.MaChuDe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuDe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuDeExists(chuDe.MaChuDe))
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
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Delete/:id
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes
                .FirstOrDefaultAsync(m => m.MaChuDe == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // POST: Admin/ChuDe/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chuDe = await _context.ChuDes.FindAsync(id);
            _context.ChuDes.Remove(chuDe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuDeExists(string id)
        {
            return _context.ChuDes.Any(e => e.MaChuDe == id);
        }
    }
}
