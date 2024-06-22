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
    public class KhachhangsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachhangsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Khachhangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Khachhang.ToListAsync());
        }

        // GET: Khachhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhang
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        public async Task<IActionResult> Searchkhachhang(string SearchOne)
        {
            var lstKhachHang = await _context.Khachhang
                                    .Where(kh => kh.Ten.Contains(SearchOne))
                                    .ToListAsync();

            return View(lstKhachHang);
        }

    }
}
