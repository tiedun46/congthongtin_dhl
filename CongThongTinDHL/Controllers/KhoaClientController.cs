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
    public class KhoaClientController : Controller
    {
        private readonly CongTTDHLuatContext _context;

        public KhoaClientController(CongTTDHLuatContext context)
        {
            _context = context;
        }

        // GET: KhoaClient
        public async Task<IActionResult> Index()
        {
            return View(await _context.Khoas.ToListAsync());
        }

        // GET: KhoaClient/Details/:id
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoa = await _context.Khoas
                .FirstOrDefaultAsync(m => m.MaKhoa == id);
            if (khoa == null)
            {
                return NotFound();
            }

            return View(khoa);
        }

    }
}
