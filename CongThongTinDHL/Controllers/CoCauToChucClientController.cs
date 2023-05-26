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
    public class CoCauToChucClientController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public CoCauToChucClientController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: CoCauToChucClient
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoCauToChucs.ToListAsync());
        }

        // GET: CoCauToChucClient/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coCauToChuc = await _context.CoCauToChucs
                .FirstOrDefaultAsync(m => m.MaTc == id);
            if (coCauToChuc == null)
            {
                return NotFound();
            }

            return View(coCauToChuc);
        }
    }
}
