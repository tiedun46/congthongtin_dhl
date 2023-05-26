using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CongThongTinDHL.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;

namespace CongThongTinDHL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoCauToChucController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoCauToChucController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/CoCauToChuc
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoCauToChucs.ToListAsync());
        }

        // GET: Admin/CoCauToChuc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coCauToChuc = await _context.CoCauToChucs
                .FirstOrDefaultAsync(m => m.MaTc == id);
            if (coCauToChuc == null)
            {
                return NotFound();
            }

            return View(coCauToChuc);
        }

        // GET: Admin/CoCauToChuc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CoCauToChuc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoCauToChuc coCauToChuc)
        {
            if (ModelState.IsValid)
            {
                if (coCauToChuc.ChoosePhoto != null)
                {
                    string folder = "images/tochuc/";
                    folder += Guid.NewGuid().ToString() + "_" + coCauToChuc.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await coCauToChuc.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    coCauToChuc.Anh = $"/{folder}";
                    coCauToChuc.NgayDang = DateTime.Now;
                    _context.Add(coCauToChuc);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                }
            }
            return View(coCauToChuc);
        }

        // GET: Admin/CoCauToChuc/Edit/:id
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coCauToChuc = await _context.CoCauToChucs.FindAsync(id);
            if (coCauToChuc == null)
            {
                return NotFound();
            }
            return View(coCauToChuc);
        }

        // POST: Admin/CoCauToChuc/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CoCauToChuc coCauToChuc)
        {
            if (id != coCauToChuc.MaTc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (coCauToChuc.ChoosePhoto != null)
                    {
                        string folder = "images/tochuc/";
                        folder += Guid.NewGuid().ToString() + "_" + coCauToChuc.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await coCauToChuc.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        coCauToChuc.Anh = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        coCauToChuc.Anh = _context.CoCauToChucs.AsNoTracking().FirstOrDefault(x => x.MaTc == coCauToChuc.MaTc)?.Anh;
                    }
                    _context.Update(coCauToChuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoCauToChucExists(coCauToChuc.MaTc))
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
            return View(coCauToChuc);
        }

        // GET: Admin/CoCauToChuc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coCauToChuc = await _context.CoCauToChucs
                .FirstOrDefaultAsync(m => m.MaTc == id);
            if (coCauToChuc == null)
            {
                return NotFound();
            }

            return View(coCauToChuc);
        }

        // POST: Admin/CoCauToChuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var coCauToChuc = await _context.CoCauToChucs.FindAsync(id);
            _context.CoCauToChucs.Remove(coCauToChuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoCauToChucExists(string id)
        {
            return _context.CoCauToChucs.Any(e => e.MaTc == id);
        }
    }
}
