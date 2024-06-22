using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MayinOnline.Data;
using MayinOnline.Models;

namespace MayinOnline.Controllers
{
    public class HoadonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoadonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hoadons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hoadon.Include(h => h.MaKhNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hoadons/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lstCTHoaDon =  _context.Cthoadon
                .Include(c => c.MaMhNavigation)
                .Where(c => c.MaHd == id);
            if (lstCTHoaDon == null)
            {
                return NotFound();
            }

            return View(lstCTHoaDon);
        }

       
    }
}
