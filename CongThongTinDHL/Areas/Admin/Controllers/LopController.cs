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
    public class LopController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public LopController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: Admin/Lop
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.Lops.Include(l => l.MaKhoaNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: Admin/Lop/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lop = await _context.Lops
                .Include(l => l.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaLop == id);
            if (lop == null)
            {
                return NotFound();
            }

            return View(lop);
        }

        // GET: Admin/Lop/Create
        public IActionResult Create()
        {
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa");
            return View();
        }

        // POST: Admin/Lop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLop,MaKhoa,TenLop,Siso,Ghichu,Status")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", lop.MaKhoa);
            return View(lop);
        }

        // GET: Admin/Lop/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lop = await _context.Lops.FindAsync(id);
            if (lop == null)
            {
                return NotFound();
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", lop.MaKhoa);
            return View(lop);
        }

        // POST: Admin/Lop/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaLop,MaKhoa,TenLop,Siso,Ghichu,Status")] Lop lop)
        {
            if (id != lop.MaLop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LopExists(lop.MaLop))
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
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", lop.MaKhoa);
            return View(lop);
        }

        // GET: Admin/Lop/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lop = await _context.Lops
                .Include(l => l.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaLop == id);
            if (lop == null)
            {
                return NotFound();
            }

            return View(lop);
        }

        // POST: Admin/Lop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lop = await _context.Lops.FindAsync(id);
            _context.Lops.Remove(lop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LopExists(string id)
        {
            return _context.Lops.Any(e => e.MaLop == id);
        }
    }
}
