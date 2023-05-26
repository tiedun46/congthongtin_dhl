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
    public class GiangVienController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GiangVienController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/GiangVien
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.GiangViens.Include(g => g.MaKhoaNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: Admin/GiangVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangViens
                .Include(g => g.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);
            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // GET: Admin/GiangVien/Create
        public IActionResult Create()
        {
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa");
            return View();
        }

        // POST: Admin/GiangVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                if (giangVien.ChoosePhoto != null)
                {
                    string folder = "images/giangvien/";
                    folder += Guid.NewGuid().ToString() + "_" + giangVien.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await giangVien.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    giangVien.AnhGv = $"/{folder}";
                    giangVien.Status = true;
                    _context.Add(giangVien);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                    return View(giangVien);
                }
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // GET: Admin/GiangVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // POST: Admin/GiangVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, GiangVien giangVien)
        {
            if (id != giangVien.MaGv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (giangVien.ChoosePhoto != null)
                    {
                        string folder = "images/giangvien/";
                        folder += Guid.NewGuid().ToString() + "_" + giangVien.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await giangVien.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        giangVien.AnhGv = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        giangVien.AnhGv = _context.GiangViens.AsNoTracking().FirstOrDefault(x => x.MaGv == giangVien.MaGv)?.AnhGv;
                    }
                    _context.Update(giangVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiangVienExists(giangVien.MaGv))
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
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // GET: Admin/GiangVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangViens
                .Include(g => g.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);
            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // POST: Admin/GiangVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            _context.GiangViens.Remove(giangVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiangVienExists(string id)
        {
            return _context.GiangViens.Any(e => e.MaGv == id);
        }
    }
}
