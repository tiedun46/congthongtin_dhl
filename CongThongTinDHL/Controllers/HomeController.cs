using CongThongTinDHL.Models;
using CongThongTinDHL.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CongThongTinDHL.Controllers
{
    public class HomeController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public HomeController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (string.IsNullOrEmpty(loginViewModel.Email) == true)
            {
                ModelState.AddModelError("", "Email không được để trống");
                return View(loginViewModel.Email);
            }
            if (string.IsNullOrEmpty(loginViewModel.Password) == true)
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
                return View(loginViewModel.Password);
            }
            var giangVien = _context.GiangViens.SingleOrDefault(x => x.Email.Trim().ToLower() == loginViewModel.Email.Trim().ToLower() && x.PasswordGv == loginViewModel.Password);
            var sinhVien = _context.SinhViens.SingleOrDefault(x => x.EmailSv.Trim().ToLower() == loginViewModel.Email.Trim().ToLower() && x.PasswordSv == loginViewModel.Password);
            var admin = _context.Admins.SingleOrDefault(x => x.Email.Trim().ToLower() == loginViewModel.Email.Trim().ToLower() && x.PasswordAd == loginViewModel.Password);
            if (sinhVien != null)
            {
                HttpContext.Session.SetString("UserID", sinhVien.MaSv.ToString());
                HttpContext.Session.SetString("FullName", sinhVien.TenSv.ToString());
                HttpContext.Session.SetString("Role", "Sinh Viên");
                if (sinhVien.AnhSv != null)
                {
                    HttpContext.Session.SetString("Image", sinhVien.AnhSv.ToString());
                }
                HttpContext.Session.SetString("Email", sinhVien.EmailSv.Trim().ToLower());
                return RedirectToAction("Index");
            }
            if (giangVien != null)
            {
                HttpContext.Session.SetString("UserID", giangVien.MaGv.ToString());
                HttpContext.Session.SetString("FullName", giangVien.HoTen.ToString());
                HttpContext.Session.SetString("Role", "Giảng Viên");
                if (giangVien.AnhGv != null)
                {
                    HttpContext.Session.SetString("Image", giangVien.AnhGv.ToString());
                }
                HttpContext.Session.SetString("Email", giangVien.Email.Trim().ToLower());
                return RedirectToAction("Index");
            }
            if (admin != null)
            {
                HttpContext.Session.SetString("UserID", admin.MaAdmin.ToString());
                HttpContext.Session.SetString("FullName", admin.Fullname.ToString());
                HttpContext.Session.SetString("Role", "Admin");
                if (admin.AvatarAd != null)
                {
                    HttpContext.Session.SetString("Image", admin.AvatarAd.ToString());
                }
                HttpContext.Session.SetString("Email", admin.Email.Trim().ToLower());
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Đăng Nhập Thất Bại! Kiểm Lại Thông Tin Đăng Nhập");
                return View();
            }
        }

        //Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
