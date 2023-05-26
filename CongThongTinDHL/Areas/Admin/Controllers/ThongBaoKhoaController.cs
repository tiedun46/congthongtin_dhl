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
    public class ThongBaoKhoaController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ThongBaoKhoaController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/ThongBaoKhoa
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.ThongBaoKhoas.Include(t => t.MaKhoaNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: Admin/ThongBaoKhoa/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoKhoa = await _context.ThongBaoKhoas
                .Include(t => t.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaTbkhoa == id);
            if (thongBaoKhoa == null)
            {
                return NotFound();
            }

            return View(thongBaoKhoa);
        }

        // GET: Admin/ThongBaoKhoa/Create
        public IActionResult Create()
        {
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa");
            return View();
        }

        // POST: Admin/ThongBaoKhoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThongBaoKhoa thongBaoKhoa)
        {
            if (ModelState.IsValid)
            {
                if (thongBaoKhoa.ChoosePhoto != null)
                {
                    string folder = "images/thongbao/";
                    folder += Guid.NewGuid().ToString() + "_" + thongBaoKhoa.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await thongBaoKhoa.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    thongBaoKhoa.AnhTb = $"/{folder}";
                    thongBaoKhoa.NgayDang = DateTime.Now;
                    _context.Add(thongBaoKhoa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                    return View(thongBaoKhoa);
                }
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", thongBaoKhoa.MaKhoa);
            return View(thongBaoKhoa);
        }

        // GET: Admin/ThongBaoKhoa/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoKhoa = await _context.ThongBaoKhoas.FindAsync(id);
            if (thongBaoKhoa == null)
            {
                return NotFound();
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", thongBaoKhoa.MaKhoa);
            return View(thongBaoKhoa);
        }

        // POST: Admin/ThongBaoKhoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ThongBaoKhoa thongBaoKhoa)
        {
            if (id != thongBaoKhoa.MaTbkhoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (thongBaoKhoa.ChoosePhoto != null)
                    {
                        string folder = "images/thongbao/";
                        folder += Guid.NewGuid().ToString() + "_" + thongBaoKhoa.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await thongBaoKhoa.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        thongBaoKhoa.AnhTb = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        thongBaoKhoa.AnhTb = _context.ThongBaoKhoas.AsNoTracking().FirstOrDefault(x => x.MaTbkhoa == thongBaoKhoa.MaTbkhoa)?.AnhTb;
                    }
                    _context.Update(thongBaoKhoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongBaoKhoaExists(thongBaoKhoa.MaTbkhoa))
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
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", thongBaoKhoa.MaKhoa);
            return View(thongBaoKhoa);
        }

        // GET: Admin/ThongBaoKhoa/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoKhoa = await _context.ThongBaoKhoas
                .Include(t => t.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaTbkhoa == id);
            if (thongBaoKhoa == null)
            {
                return NotFound();
            }

            return View(thongBaoKhoa);
        }

        // POST: Admin/ThongBaoKhoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var thongBaoKhoa = await _context.ThongBaoKhoas.FindAsync(id);
            _context.ThongBaoKhoas.Remove(thongBaoKhoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongBaoKhoaExists(string id)
        {
            return _context.ThongBaoKhoas.Any(e => e.MaTbkhoa == id);
        }
    }
}
