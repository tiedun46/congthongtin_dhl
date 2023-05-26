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
using Microsoft.AspNetCore.Http;

namespace CongThongTinDHL.Controllers
{
    public class SinhVienDashboardController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SinhVienDashboardController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: SinhVienDashboard
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.SinhViens.Include(s => s.MaKhoaNavigation).Include(s => s.MaLopNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: SinhVienDashboard/Edit/5
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

        // POST: SinhVienDashboard/Edit/5
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
                return RedirectToAction("Index", "Home");
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", sinhVien.MaKhoa);
            ViewData["MaLop"] = new SelectList(_context.Lops, "MaLop", "MaLop", sinhVien.MaLop);
            return View(sinhVien);
        }


        public IActionResult ChangPassWordSV()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangPassWordSV(string oldPassword, string newPassword, string cfPassword)
        {
            var userId = HttpContext.Session.GetString("UserID");
            var sinhVien = await _context.SinhViens.FindAsync(userId);
            if (sinhVien != null)
            {
                if (oldPassword == null)
                {
                    ModelState.AddModelError("", "Mật khẩu không được để trống");
                    return View();
                }
                if (newPassword == null)
                {
                    ModelState.AddModelError("", "Mật khẩu không được để trống");
                    return View();
                }
                if (cfPassword == null)
                {
                    ModelState.AddModelError("", "Mật khẩu không được để trống");
                    return View();
                }
                if (sinhVien.PasswordSv == oldPassword)
                {
                    if (newPassword == cfPassword)
                    {
                        sinhVien.PasswordSv = newPassword;
                        _context.Update(sinhVien);
                        var check = _context.SaveChanges();
                        if (check > 0)
                        {
                            return RedirectToAction("Details");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lỗi lưu dữ liệu");
                            return View();
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu nhập lại không khớp");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                    return View();
                }
            }
            else return RedirectToAction("Login", "Home");
        }

        private bool SinhVienExists(string id)
        {
            return _context.SinhViens.Any(e => e.MaSv == id);
        }
    }
}
