using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CongThongTinDHL.Models;

namespace CongThongTinDHL.Controllers
{
    public class ThongBaoKhoaController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public ThongBaoKhoaController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: ThongBaoKhoa
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.ThongBaoKhoas.Include(t => t.MaKhoaNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: ThongBaoKhoa/Details/5
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
    }
}
