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
    public class PhongClientController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public PhongClientController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: PhongClient
        public async Task<IActionResult> Index()
        {
            return View(await _context.Phongs.ToListAsync());
        }

        // GET: PhongClient/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs
                .FirstOrDefaultAsync(m => m.MaPhong == id);
            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }

        
    }
}
