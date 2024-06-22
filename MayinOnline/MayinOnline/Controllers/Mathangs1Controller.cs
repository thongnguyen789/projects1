using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MayinOnline.Data;
using MayinOnline.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MayinOnline.Controllers
{
    public class Mathangs1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Mathangs1Controller(ApplicationDbContext context)
        {
            _context = context;
        }


        public void GetInfo()
        {
            string email = HttpContext.Session.GetString("nhanvien");
            ViewBag.nhanvien = _context.Nhanvien.FirstOrDefault(n => n.Email == email);
            ViewBag.mathang = _context.Mathang.ToList();
            if (HttpContext.Session.GetString("khachhang") != "")
            {
                ViewBag.khachhang = _context.Khachhang.FirstOrDefault(k => k.Email == HttpContext.Session.GetString("khachhang"));
            }
        }

        // GET: Mathangs1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mathang.Include(m => m.MaDmNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mathangs1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathang = await _context.Mathang
                .Include(m => m.MaDmNavigation)
                .FirstOrDefaultAsync(m => m.MaMh == id);
            if (mathang == null)
            {
                return NotFound();
            }

            return View(mathang);
        }

        // GET: Mathangs1/Create
        public IActionResult Create()
        {
            ViewData["MaDm"] = new SelectList(_context.Danhmuc, "MaDm", "Ten");
            return View();
        }

        // POST: Mathangs1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMh,Ten,GiaGoc,GiaBan,SoLuong,MoTa,HinhAnh,Hinh1,Hinh2,Hinh3,MaDm,LuotXem,LuotMua")] Mathang mathang, IFormFile file, IFormFile file1, IFormFile file2, IFormFile file3)
        {
            if (ModelState.IsValid)
            {
                mathang.HinhAnh = Upload(file);
                mathang.Hinh1 = Upload1(file1);
                mathang.Hinh2 = Upload2(file2);
                mathang.Hinh3 = Upload3(file3);

                _context.Add(mathang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDm"] = new SelectList(_context.Danhmuc, "MaDm", "Ten", mathang.MaDm);
            return View(mathang);
        }


        public string Upload(IFormFile file)
        {
            string uploadFileName = null;
            if (file != null)
            {

                uploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\Template\\Picture\\{uploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            }
            return uploadFileName;
        }
        public string Upload1(IFormFile file1)
        {
            string uploadFileName = null;
            if (file1 != null)
            {

                uploadFileName = Guid.NewGuid().ToString() + "_" + file1.FileName;
                var path = $"wwwroot\\Template\\Picture\\{uploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file1.CopyTo(stream);
                }

            }
            return uploadFileName;
        }

        public string Upload2(IFormFile file2)
        {
            string uploadFileName = null;
            if (file2 != null)
            {

                uploadFileName = Guid.NewGuid().ToString() + "_" + file2.FileName;
                var path = $"wwwroot\\Template\\Picture\\{uploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file2.CopyTo(stream);
                }

            }
            return uploadFileName;
        }
        public string Upload3(IFormFile file3)
        {
            string uploadFileName = null;
            if (file3 != null)
            {

                uploadFileName = Guid.NewGuid().ToString() + "_" + file3.FileName;
                var path = $"wwwroot\\Template\\Picture\\{uploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file3.CopyTo(stream);
                }

            }
            return uploadFileName;
        }


        // GET: Mathangs1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathang = await _context.Mathang.FindAsync(id);
            if (mathang == null)
            {
                return NotFound();
            }
            ViewData["MaDm"] = new SelectList(_context.Danhmuc, "MaDm", "Ten", mathang.MaDm);
            return View(mathang);
        }

        // POST: Mathangs1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMh,Ten,GiaGoc,GiaBan,SoLuong,MoTa,HinhAnh,Hinh1,Hinh2,Hinh3,MaDm,LuotXem,LuotMua")] Mathang mathang)
        {
            if (id != mathang.MaMh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mathang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MathangExists(mathang.MaMh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDm"] = new SelectList(_context.Danhmuc, "MaDm", "Ten", mathang.MaDm);
            return View(mathang);
        }

        // GET: Mathangs1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mathang = await _context.Mathang
                .Include(m => m.MaDmNavigation)
                .FirstOrDefaultAsync(m => m.MaMh == id);
            if (mathang == null)
            {
                return NotFound();
            }

            return View(mathang);
        }

        // POST: Mathangs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mathang = await _context.Mathang.FindAsync(id);
            _context.Mathang.Remove(mathang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MathangExists(int id)
        {
            return _context.Mathang.Any(e => e.MaMh == id);
        }

        public async Task<IActionResult> Searchmathang(string SearchKey)
        {
            var lstHang = await _context.Mathang.Include(m => m.MaDmNavigation)
                            .Where(k => k.Ten.Contains(SearchKey)).ToListAsync();
            GetInfo();
            return View(lstHang);
        }

    }
}
