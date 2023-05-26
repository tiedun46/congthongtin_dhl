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
    public class SinhVienController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SinhVienController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/SinhVien
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.SinhViens.Include(s => s.MaKhoaNavigation).Include(s => s.MaLopNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: Admin/SinhVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .Include(s => s.MaKhoaNavigation)
                .Include(s => s.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaSv == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: Admin/SinhVien/Create
        public IActionResult Create()
        {
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa");
            ViewData["MaLop"] = new SelectList(_context.Lops, "MaLop", "MaLop");
            return View();
        }

        // POST: Admin/SinhVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                if (sinhVien.ChoosePhoto != null)
                {
                    string folder = "images/sinhvien/";
                    folder += Guid.NewGuid().ToString() + "_" + sinhVien.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await sinhVien.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    sinhVien.AnhSv = $"/{folder}";
                    sinhVien.Status = true;
                    _context.Add(sinhVien);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                    return View(sinhVien);
                }
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", sinhVien.MaKhoa);
            ViewData["MaLop"] = new SelectList(_context.Lops, "MaLop", "MaLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: Admin/SinhVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", sinhVien.MaKhoa);
            ViewData["MaLop"] = new SelectList(_context.Lops, "MaLop", "MaLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // POST: Admin/SinhVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SinhVien sinhVien)
        {
            if (id != sinhVien.MaSv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (sinhVien.ChoosePhoto != null)
                    {
                        string folder = "images/sinhvien/";
                        folder += Guid.NewGuid().ToString() + "_" + sinhVien.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await sinhVien.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        sinhVien.AnhSv = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        sinhVien.AnhSv = _context.SinhViens.AsNoTracking().FirstOrDefault(x => x.MaSv == sinhVien.MaSv)?.AnhSv;
                    }
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.MaSv))
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
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", sinhVien.MaKhoa);
            ViewData["MaLop"] = new SelectList(_context.Lops, "MaLop", "MaLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: Admin/SinhVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .Include(s => s.MaKhoaNavigation)
                .Include(s => s.MaLopNavigation)
                .FirstOrDefaultAsync(m => m.MaSv == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: Admin/SinhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            _context.SinhViens.Remove(sinhVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhVienExists(string id)
        {
            return _context.SinhViens.Any(e => e.MaSv == id);
        }
    }
}
