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
    public class KhoaController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public KhoaController(CongTTDHLuatContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Khoa
        public async Task<IActionResult> Index()
        {
            return View(await _context.Khoas.ToListAsync());
        }

        // GET: Admin/Khoa/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoa = await _context.Khoas
                .FirstOrDefaultAsync(m => m.MaKhoa == id);
            if (khoa == null)
            {
                return NotFound();
            }

            return View(khoa);
        }

        // GET: Admin/Khoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Khoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                if (khoa.ChoosePhoto != null)
                {
                    string folder = "images/khoa/";
                    folder += Guid.NewGuid().ToString() + "_" + khoa.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await khoa.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    khoa.AvatarKhoa = $"/{folder}";
                    _context.Add(khoa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                    return View(khoa);
                }
            }
            return View(khoa);
        }

        // GET: Admin/Khoa/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null)
            {
                return NotFound();
            }
            return View(khoa);
        }

        // POST: Admin/Khoa/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Khoa khoa)
        {
            if (id != khoa.MaKhoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (khoa.ChoosePhoto != null)
                    {
                        string folder = "images/khoa/";
                        folder += Guid.NewGuid().ToString() + "_" + khoa.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await khoa.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        khoa.AvatarKhoa = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        khoa.AvatarKhoa = _context.Khoas.AsNoTracking().FirstOrDefault(x => x.MaKhoa == khoa.MaKhoa)?.AvatarKhoa;
                    }
                    _context.Update(khoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaExists(khoa.MaKhoa))
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
            return View(khoa);
        }

        // GET: Admin/Khoa/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoa = await _context.Khoas
                .FirstOrDefaultAsync(m => m.MaKhoa == id);
            if (khoa == null)
            {
                return NotFound();
            }

            return View(khoa);
        }

        // POST: Admin/Khoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            _context.Khoas.Remove(khoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhoaExists(string id)
        {
            return _context.Khoas.Any(e => e.MaKhoa == id);
        }
    }
}
