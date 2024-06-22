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
using System.Diagnostics;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace MayinOnline.Controllers
{
    public class MathangsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MathangsController(ApplicationDbContext context)
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


        // GET: Mathangs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mathang.Include(m => m.MaDmNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            GetInfo();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Tongdoanhthu()
        {
            var applicationDbContext = _context.Cthoadon.Include(m => m.MaHdNavigation);
            int tongtien = 0;
            foreach (Cthoadon cthd in applicationDbContext)
            {
                tongtien += cthd.ThanhTien;
            }
            ViewData["tongtien"] = tongtien.ToString("n0");
            GetInfo();
            return View(applicationDbContext);
        }

        [HttpGet]
        public async Task<FileResult> excel()
        {

            List<Hoadon> hd = await _context.Hoadon.Include(m =>m.MaKhNavigation).ToListAsync();
            var fil = "Hoadon.xlsx";
            return Excel(fil, hd);
        }

        private FileResult Excel(string fil, IEnumerable<Hoadon> hd)
        {
            DataTable data = new DataTable("hd");
            data.Columns.AddRange(new DataColumn[]
            {
              new DataColumn("Mã hóa đơn"),
              new DataColumn("Mã khách hàng"),
              new DataColumn("Tên khách hàng"),
              new DataColumn("Ngày lập"),
              new DataColumn("Email"),
              new DataColumn("Số điện thoại"),
              new DataColumn("Tổng tền")

            });

            foreach (var p in hd)
            {
                data.Rows.Add(p.MaHd, p.MaKh, p.MaKhNavigation.Ten, p.Ngay, p.MaKhNavigation.Email, p.MaKhNavigation.DienThoai, p.TongTien);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(data);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fil);
                }
            }
        }
        public IActionResult ThongKeKhachHang()
        {
            GetInfo();
            return View();
        }

        public IActionResult XemThongKeKhachHang(string emailkhachhang)
        {
            var lstHoaDon = _context.Hoadon.Include(m => m.MaKhNavigation)
                            .Where(h => h.MaKhNavigation.Email == emailkhachhang && h.MaKhNavigation.MatKhau != null);
            int tongtien = 0;
            foreach (Hoadon hd in lstHoaDon)
            {
                tongtien += hd.TongTien;
            }
            ViewBag.ttkhachhang = _context.Khachhang.FirstOrDefault(k => k.Email == emailkhachhang && k.MatKhau != null);
            ViewData["tongtien"] = tongtien.ToString("n0");
            GetInfo();
            return View(lstHoaDon);
        }
        public IActionResult ThongKeTheoNgay()
        {
            GetInfo();
            return View();
        }

        [HttpPost]
        public IActionResult XemThongKeTheoNgay(DateTime Ngaybatdau, DateTime Ngayketthuc)
        {
            var lstHD = _context.Hoadon.Include(m => m.MaKhNavigation.Hoadon).Where(d => d.Ngay >= Ngaybatdau && d.Ngay <= Ngayketthuc);
            int tongtien = 0;
            foreach (Hoadon hd in lstHD)
            {
                tongtien += hd.TongTien;
            }
            ViewData["ngaybatdau"] = Ngaybatdau.Month.ToString() + "/" + Ngaybatdau.Day.ToString() + "/" + Ngaybatdau.Year.ToString();
            ViewData["ngayketthuc"] = Ngayketthuc.Month.ToString() + "/" + Ngayketthuc.Day.ToString() + "/" + Ngayketthuc.Year.ToString();
            ViewData["tongtien"] = tongtien.ToString("n0");
            GetInfo();
            return View(lstHD);
        }

        [HttpPost]
        public List<object> Get()
        {
            List<object> data = new List<object>();
            List<DateTime> labels = _context.Hoadon.Select(m => m.Ngay).ToList();
            List<int> total = _context.Hoadon.Select(m => m.TongTien).ToList();
            data.Add(labels);
            data.Add(total);
            return data;
        }
    }
}
