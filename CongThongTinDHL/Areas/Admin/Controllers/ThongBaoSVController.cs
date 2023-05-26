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
    public class ThongBaoSVController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ThongBaoSVController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/ThongBaoSV
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThongBaoSvs.ToListAsync());
        }

        // GET: Admin/ThongBaoSV/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoSv = await _context.ThongBaoSvs
                .FirstOrDefaultAsync(m => m.MaTbsv == id);
            if (thongBaoSv == null)
            {
                return NotFound();
            }

            return View(thongBaoSv);
        }

        // GET: Admin/ThongBaoSV/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThongBaoSV/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThongBaoSv thongBao)
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
            return View(thongBao);
        }

        // GET: Admin/ThongBaoSV/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoSv = await _context.ThongBaoSvs.FindAsync(id);
            if (thongBaoSv == null)
            {
                return NotFound();
            }
            return View(thongBaoSv);
        }

        // POST: Admin/ThongBaoSV/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ThongBaoSv thongBao)
        {
            if (id != thongBao.MaTbsv)
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
                        thongBao.AnhTb = _context.ThongBaoSvs.AsNoTracking().FirstOrDefault(x => x.MaTbsv == thongBao.MaTbsv)?.AnhTb;
                    }
                    _context.Update(thongBao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongBaoSvExists(thongBao.MaTbsv))
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
            return View(thongBao);
        }

        // GET: Admin/ThongBaoSV/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBaoSv = await _context.ThongBaoSvs
                .FirstOrDefaultAsync(m => m.MaTbsv == id);
            if (thongBaoSv == null)
            {
                return NotFound();
            }

            return View(thongBaoSv);
        }

        // POST: Admin/ThongBaoSV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var thongBaoSv = await _context.ThongBaoSvs.FindAsync(id);
            _context.ThongBaoSvs.Remove(thongBaoSv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongBaoSvExists(string id)
        {
            return _context.ThongBaoSvs.Any(e => e.MaTbsv == id);
        }
    }
}
