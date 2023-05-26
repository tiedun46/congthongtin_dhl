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
    public class GioiThieuClientController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public GioiThieuClientController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: GioiThieuClient
        public async Task<IActionResult> Index()
        {
            return View(await _context.GioiThieus.ToListAsync());
        }

        // GET: GioiThieuClient/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieu = await _context.GioiThieus
                .FirstOrDefaultAsync(m => m.MaGt == id);
            if (gioiThieu == null)
            {
                return NotFound();
            }

            return View(gioiThieu);
        }

        
    }
}
