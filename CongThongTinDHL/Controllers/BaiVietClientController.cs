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
    public class BaiVietClientController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public BaiVietClientController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: BaiVietClient
        public async Task<IActionResult> Index()
        {
            var congTTDHLuatContext = _context.BaiViets.Include(b => b.MaChuDeNavigation);
            return View(await congTTDHLuatContext.ToListAsync());
        }

        // GET: BaiVietClient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets
                .Include(b => b.MaChuDeNavigation)
                .FirstOrDefaultAsync(m => m.MaBv == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // GET: BlogsList/MaChuDe
        public async Task<IActionResult> BlogsChuDe(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViets = await _context.BaiViets
                .Include(b => b.MaChuDeNavigation)
                .Where(m => m.MaChuDe == id)
                .ToListAsync();
            if (baiViets == null)
            {
                return NotFound();
            }

            return View(baiViets);
        }

    }
}
