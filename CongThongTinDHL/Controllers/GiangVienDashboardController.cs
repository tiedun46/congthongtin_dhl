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
    public class GiangVienDashboardController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GiangVienDashboardController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: GiangVienDashboard/Edit/5
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

        // POST: GiangVienDashboard/Edit/5
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

        public IActionResult ChangPassWordGV()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangPassWordGV(string oldPassword, string newPassword, string cfPassword)
        {
            var userId = HttpContext.Session.GetString("UserID");
            var giangVien = await _context.GiangViens.FindAsync(userId);
            if (giangVien != null)
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
                if (giangVien.PasswordGv == oldPassword)
                {
                    if (newPassword == cfPassword)
                    {
                        giangVien.PasswordGv = newPassword;
                        _context.Update(giangVien);
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

        private bool GiangVienExists(string id)
        {
            return _context.GiangViens.Any(e => e.MaGv == id);
        }
    }
}
