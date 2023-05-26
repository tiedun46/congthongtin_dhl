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
    public class ThongBaoPhongController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ThongBaoPhongController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/ThongBaoPhong
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.ThongBaoPhongs.Include(t => t.MaPhongNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: Admin/ThongBaoPhong/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoPhong = await _context.ThongBaoPhongs
                .Include(t => t.MaPhongNavigation)
                .FirstOrDefaultAsync(m => m.MaTbphong == id);
            if (thongBaoPhong == null)
            {
                return NotFound();
            }

            return View(thongBaoPhong);
        }

        // GET: Admin/ThongBaoPhong/Create
        public IActionResult Create()
        {
            ViewData["MaPhong"] = new SelectList(_context.Phongs, "MaPhong", "MaPhong");
            return View();
        }

        // POST: Admin/ThongBaoPhong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThongBaoPhong thongBao)
        {
            if (ModelState.IsValid)
            {
                if (thongBao.ChoosePhoto != null)
                {
                    string folder = "images/thongbao/";
                    folder += Guid.NewGuid().ToString() + "_" + thongBao.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await thongBao.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    thongBao.AnhTb = $"/{folder}";
                    thongBao.NgayDang = DateTime.Now;
                    _context.Add(thongBao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                    return View(thongBao);
                }
            }
            ViewData["MaPhong"] = new SelectList(_context.Phongs, "MaPhong", "MaPhong", thongBao.MaPhong);
            return View(thongBao);
        }

        // GET: Admin/ThongBaoPhong/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoPhong = await _context.ThongBaoPhongs.FindAsync(id);
            if (thongBaoPhong == null)
            {
                return NotFound();
            }
            ViewData["MaPhong"] = new SelectList(_context.Phongs, "MaPhong", "MaPhong", thongBaoPhong.MaPhong);
            return View(thongBaoPhong);
        }

        // POST: Admin/ThongBaoPhong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ThongBaoPhong thongBao)
        {
            if (id != thongBao.MaTbphong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (thongBao.ChoosePhoto != null)
                    {
                        string folder = "images/thongbao/";
                        folder += Guid.NewGuid().ToString() + "_" + thongBao.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await thongBao.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        thongBao.AnhTb = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        thongBao.AnhTb = _context.ThongBaoPhongs.AsNoTracking().FirstOrDefault(x => x.MaTbphong == thongBao.MaTbphong)?.AnhTb;
                    }
                    _context.Update(thongBao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongBaoPhongExists(thongBao.MaTbphong))
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
            ViewData["MaPhong"] = new SelectList(_context.Phongs, "MaPhong", "MaPhong", thongBao.MaPhong);
            return View(thongBao);
        }

        // GET: Admin/ThongBaoPhong/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoPhong = await _context.ThongBaoPhongs
                .Include(t => t.MaPhongNavigation)
                .FirstOrDefaultAsync(m => m.MaTbphong == id);
            if (thongBaoPhong == null)
            {
                return NotFound();
            }

            return View(thongBaoPhong);
        }

        // POST: Admin/ThongBaoPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var thongBaoPhong = await _context.ThongBaoPhongs.FindAsync(id);
            _context.ThongBaoPhongs.Remove(thongBaoPhong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongBaoPhongExists(string id)
        {
            return _context.ThongBaoPhongs.Any(e => e.MaTbphong == id);
        }
    }
}
