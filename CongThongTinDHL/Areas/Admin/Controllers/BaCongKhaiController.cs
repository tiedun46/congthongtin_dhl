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
    public class BaCongKhaiController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public BaCongKhaiController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: Admin/BaCongKhai
        public async Task<IActionResult> Index()
        {
            return View(await _context.BaCongKhais.ToListAsync());
        }

        // GET: Admin/BaCongKhai/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baCongKhai = await _context.BaCongKhais
                .FirstOrDefaultAsync(m => m.MaBck == id);
            if (baCongKhai == null)
            {
                return NotFound();
            }

            return View(baCongKhai);
        }

        // GET: Admin/BaCongKhai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BaCongKhai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBck,TenBck,Link,MoTaNgan,MoChiTiet,NgayDang,Status")] BaCongKhai baCongKhai)
        {
            if (ModelState.IsValid)
            {
                baCongKhai.NgayDang = DateTime.Now;
                _context.Add(baCongKhai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baCongKhai);
        }

        // GET: Admin/BaCongKhai/Edit/:id
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baCongKhai = await _context.BaCongKhais.FindAsync(id);
            if (baCongKhai == null)
            {
                return NotFound();
            }
            return View(baCongKhai);
        }

        // POST: Admin/BaCongKhai/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaBck,TenBck,Link,MoTaNgan,MoChiTiet,NgayDang,Status")] BaCongKhai baCongKhai)
        {
            if (id != baCongKhai.MaBck)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baCongKhai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaCongKhaiExists(baCongKhai.MaBck))
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
            return View(baCongKhai);
        }

        // GET: Admin/BaCongKhai/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baCongKhai = await _context.BaCongKhais
                .FirstOrDefaultAsync(m => m.MaBck == id);
            if (baCongKhai == null)
            {
                return NotFound();
            }

            return View(baCongKhai);
        }

        // POST: Admin/BaCongKhai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var baCongKhai = await _context.BaCongKhais.FindAsync(id);
            _context.BaCongKhais.Remove(baCongKhai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaCongKhaiExists(string id)
        {
            return _context.BaCongKhais.Any(e => e.MaBck == id);
        }
    }
}
