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
    public class AdminDashboardController : Controller
    {
        private readonly CongTTDHLuatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminDashboardController(CongTTDHLuatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: AdminDashboard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: AdminDashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Admin admin)
        {
            if (id != admin.MaAdmin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (admin.ChoosePhoto != null)
                    {
                        string folder = "images/admin/";
                        folder += Guid.NewGuid().ToString() + "_" + admin.ChoosePhoto.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await admin.ChoosePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        admin.AvatarAd = $"/{folder}";
                    }
                    else // giữ nguyên giá trị của thuộc tính Anh
                    {
                        admin.AvatarAd = _context.Admins.AsNoTracking().FirstOrDefault(x => x.MaAdmin == admin.MaAdmin)?.AvatarAd;
                    }
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.MaAdmin))
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
            return View(admin);
        }

        public IActionResult ChangPassWordAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangPassWordAdmin(string oldPassword, string newPassword, string cfPassword)
        {
            var userId = HttpContext.Session.GetString("UserID");
            var admin = await _context.Admins.FindAsync(int.Parse(userId));
            if (admin != null)
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
                if (admin.PasswordAd == oldPassword)
                {
                    if (newPassword == cfPassword)
                    {
                        admin.PasswordAd = newPassword;
                        _context.Update(admin);
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


        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.MaAdmin == id);
        }
    }
}
