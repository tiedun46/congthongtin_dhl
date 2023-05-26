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
    public class BaiVietController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BaiVietController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/BaiViet
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.BaiViets.Include(b => b.MaChuDeNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: Admin/BaiViet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets
                .Include(b => b.MaChuDeNavigation)
                .FirstOrDefaultAsync(m => m.MaBv == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // GET: Admin/BaiViet/Create
        public IActionResult Create()
        {
            ViewData["MaChuDe"] = new SelectList(_context.ChuDes, "MaChuDe", "TenCd");
            return View();
        }

        // POST: Admin/BaiViet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                if (baiViet.ChoosePhoto != null)
                {
                    string folder = "images/baiviet/";
                    folder += Guid.NewGuid().ToString() + "_" + baiViet.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await baiViet.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    baiViet.AnhBv = $"/{folder}";
                    baiViet.NgayDang = DateTime.Now;
                    _context.Add(baiViet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                    return View(baiViet);
                }
                
            }
            ViewData["MaChuDe"] = new SelectList(_context.ChuDes, "MaChuDe", "MaChuDe", baiViet.MaChuDe);
            return View(baiViet);
        }

        // GET: Admin/BaiViet/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets.FindAsync(id);
            if (baiViet == null)
            {
                return NotFound();
            }
            ViewData["MaChuDe"] = new SelectList(_context.ChuDes, "MaChuDe", "TenCd", baiViet.MaChuDe);
            return View(baiViet);
        }

        // POST: Admin/BaiViet/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BaiViet baiViet)
        {
            if (id != baiViet.MaBv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (baiViet.ChoosePhoto != null)
                    {
                        string folder = "images/baiviet/";
                        folder += Guid.NewGuid().ToString() + "_" + baiViet.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await baiViet.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        baiViet.AnhBv = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        baiViet.AnhBv = _context.BaiViets.AsNoTracking().FirstOrDefault(x => x.MaBv == baiViet.MaBv)?.AnhBv;
                    }
                    _context.Update(baiViet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiVietExists(baiViet.MaBv))
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
            ViewData["MaChuDe"] = new SelectList(_context.ChuDes, "MaChuDe", "MaChuDe", baiViet.MaChuDe);
            return View(baiViet);
        }

        // GET: Admin/BaiViet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets
                .Include(b => b.MaChuDeNavigation)
                .FirstOrDefaultAsync(m => m.MaBv == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // POST: Admin/BaiViet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baiViet = await _context.BaiViets.FindAsync(id);
            _context.BaiViets.Remove(baiViet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiVietExists(int id)
        {
            return _context.BaiViets.Any(e => e.MaBv == id);
        }
    }
}
