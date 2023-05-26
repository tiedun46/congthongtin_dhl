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
    public class ThongBaoPhongController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public ThongBaoPhongController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: ThongBaoPhong
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.ThongBaoPhongs.Include(t => t.MaPhongNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: ThongBaoPhong/Details/5
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

        
    }
}
