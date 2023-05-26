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

namespace CongThongTinDHL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GioiThieuController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GioiThieuController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/GioiThieu
        public async Task<IActionResult> Index()
        {
            return View(await _context.GioiThieus.ToListAsync());
        }

        // GET: Admin/GioiThieu/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieu = await _context.GioiThieus
                .FirstOrDefaultAsync(m => m.MaGt == id);
            if (gioiThieu == null)
            {
                return NotFound();
            }

            return View(gioiThieu);
        }

        // GET: Admin/GioiThieu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GioiThieu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GioiThieu gioiThieu)
        {
            if (ModelState.IsValid)
            {
                if (gioiThieu.ChoosePhoto != null)
                {
                    string folder = "images/gioithieu/";
                    folder += Guid.NewGuid().ToString() + "_" + gioiThieu.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await gioiThieu.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    gioiThieu.Anh = $"/{folder}";
                    gioiThieu.NgayDang = DateTime.Now;
                    _context.Add(gioiThieu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                    return View(gioiThieu);
                }
            }
            return View(gioiThieu);
        }

        // GET: Admin/GioiThieu/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieu = await _context.GioiThieus.FindAsync(id);
            if (gioiThieu == null)
            {
                return NotFound();
            }
            return View(gioiThieu);
        }

        // POST: Admin/GioiThieu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, GioiThieu gioiThieu)
        {
            if (id != gioiThieu.MaGt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (gioiThieu.ChoosePhoto != null)
                    {
                        string folder = "images/gioithieu/";
                        folder += Guid.NewGuid().ToString() + "_" + gioiThieu.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await gioiThieu.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        gioiThieu.Anh = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        gioiThieu.Anh = _context.GioiThieus.AsNoTracking().FirstOrDefault(x => x.MaGt == gioiThieu.MaGt)?.Anh;
                    }
                    _context.Update(gioiThieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioiThieuExists(gioiThieu.MaGt))
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
            return View(gioiThieu);
        }

        // GET: Admin/GioiThieu/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieu = await _context.GioiThieus
                .FirstOrDefaultAsync(m => m.MaGt == id);
            if (gioiThieu == null)
            {
                return NotFound();
            }

            return View(gioiThieu);
        }

        // POST: Admin/GioiThieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gioiThieu = await _context.GioiThieus.FindAsync(id);
            _context.GioiThieus.Remove(gioiThieu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioiThieuExists(string id)
        {
            return _context.GioiThieus.Any(e => e.MaGt == id);
        }
    }
}
