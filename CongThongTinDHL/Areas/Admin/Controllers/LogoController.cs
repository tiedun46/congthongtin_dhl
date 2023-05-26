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
    public class LogoController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LogoController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Logo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Logos.ToListAsync());
        }

        // GET: Admin/Logo/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logo = await _context.Logos
                .FirstOrDefaultAsync(m => m.MaLogo == id);
            if (logo == null)
            {
                return NotFound();
            }

            return View(logo);
        }

        // GET: Admin/Logo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Logo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Logo logo)
        {
            if (ModelState.IsValid)
            {
                if (logo.ChoosePhoto != null)
                {
                    string folder = "images/logo/";
                    folder += Guid.NewGuid().ToString() + "_" + logo.ChoosePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await logo.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    logo.AvatarLogo = $"/{folder}";
                }
                else                
                {
                    ModelState.AddModelError("", "Bạn chưa chọn ảnh");
                }
                logo.NgayTao = DateTime.Now;
                _context.Add(logo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(logo);
        }

        // GET: Admin/Logo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logo = await _context.Logos.FindAsync(id);
            if (logo == null)
            {
                return NotFound();
            }
            return View(logo);
        }

        // POST: Admin/Logo/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Logo logo)
        {
            if (id != logo.MaLogo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (logo.ChoosePhoto != null)
                    {
                        string folder = "images/logo/";
                        folder += Guid.NewGuid().ToString() + "_" + logo.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await logo.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        logo.AvatarLogo = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        logo.AvatarLogo = _context.Logos.AsNoTracking().FirstOrDefault(x => x.MaLogo == logo.MaLogo)?.AvatarLogo;
                    }
                    _context.Update(logo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogoExists(logo.MaLogo))
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
            return View(logo);
        }

        // GET: Admin/Logo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logo = await _context.Logos
                .FirstOrDefaultAsync(m => m.MaLogo == id);
            if (logo == null)
            {
                return NotFound();
            }

            return View(logo);
        }

        // POST: Admin/Logo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logo = await _context.Logos.FindAsync(id);
            _context.Logos.Remove(logo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogoExists(int id)
        {
            return _context.Logos.Any(e => e.MaLogo == id);
        }
    }
}
