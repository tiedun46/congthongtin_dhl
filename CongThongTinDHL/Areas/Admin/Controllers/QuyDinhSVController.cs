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
    public class QuyDinhSVController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public QuyDinhSVController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: Admin/QuyDinhSV
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuyDinhSvs.ToListAsync());
        }

        // GET: Admin/QuyDinhSV/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quyDinhSv = await _context.QuyDinhSvs
                .FirstOrDefaultAsync(m => m.MaQdsv == id);
            if (quyDinhSv == null)
            {
                return NotFound();
            }

            return View(quyDinhSv);
        }

        // GET: Admin/QuyDinhSV/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuyDinhSV/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaQdsv,TenQd,MoTaNgan,MoChiTiet,NgayDang,Status")] QuyDinhSv quyDinhSv)
        {
            if (ModelState.IsValid)
            {
                quyDinhSv.NgayDang = DateTime.Now;
                _context.Add(quyDinhSv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quyDinhSv);
        }

        // GET: Admin/QuyDinhSV/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quyDinhSv = await _context.QuyDinhSvs.FindAsync(id);
            if (quyDinhSv == null)
            {
                return NotFound();
            }
            return View(quyDinhSv);
        }

        // POST: Admin/QuyDinhSV/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaQdsv,TenQd,MoTaNgan,MoChiTiet,NgayDang,Status")] QuyDinhSv quyDinhSv)
        {
            if (id != quyDinhSv.MaQdsv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quyDinhSv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuyDinhSvExists(quyDinhSv.MaQdsv))
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
            return View(quyDinhSv);
        }

        // GET: Admin/QuyDinhSV/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quyDinhSv = await _context.QuyDinhSvs
                .FirstOrDefaultAsync(m => m.MaQdsv == id);
            if (quyDinhSv == null)
            {
                return NotFound();
            }

            return View(quyDinhSv);
        }

        // POST: Admin/QuyDinhSV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var quyDinhSv = await _context.QuyDinhSvs.FindAsync(id);
            _context.QuyDinhSvs.Remove(quyDinhSv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuyDinhSvExists(string id)
        {
            return _context.QuyDinhSvs.Any(e => e.MaQdsv == id);
        }
    }
}
