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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace MayinOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Khachhang> _pwHear;

        public HomeController(ApplicationDbContext context, IPasswordHasher<Khachhang> passwordHasher)
        {
            _context = context;
            _pwHear = passwordHasher;
        }


        void GetInfo()
        {
            // số lượng mặt hàng có trong giỏ   
            ViewData["solg"] = GetCartItems().Count();
            // danh sách danh mục có trong db
            ViewBag.danhmuc = _context.Danhmuc.ToList();
            // lấy thông tin người dùng
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("khachhang")))
            {
                ViewBag.khachhang = _context.Khachhang.FirstOrDefault(k => k.Email == HttpContext.Session.GetString("khachhang"));
                if (ViewBag.khachhang == null)
                {
                    ViewBag.khachhang = new Khachhang();
                }
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Nhanvien")))
            {
                ViewBag.Nhanvien = _context.Nhanvien.FirstOrDefault(k => k.Email == HttpContext.Session.GetString("Nhanvien"));
                if (ViewBag.Nhanvien == null)
                {
                    ViewBag.Nhanvien = new Nhanvien();
                }
            }

        }


        // GET: Home
        public async Task<IActionResult> Index()
        {
            GetInfo();
            var applicationDbContext = _context.Mathang.Include(m => m.MaDmNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // lấy sản phẩm theo danh mục
        public async Task<IActionResult> DanhmucSP(int id)
        {
            GetInfo();
            var applicationDbContext = _context.Mathang.Where(s => s.MaDm == id).Include(m => m.MaDmNavigation);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Home/Details/5
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

            //Tăng lượt xem
            mathang.LuotXem++;
            _context.SaveChanges();

            GetInfo();
            return View(mathang);
        }


        //Giới thiệu
        public IActionResult GioiThieu()
        {
            GetInfo();
            return View();
        }

        public IActionResult GioiThieuBrother()
        {
            GetInfo();
            return View();
        }

        public IActionResult GioiThieuCanon()
        {
            GetInfo();
            return View();
        }

        public IActionResult GioiThieuHp()
        {
            GetInfo();
            return View();
        }

        public IActionResult GioiThieuEpson()
        {
            GetInfo();
            return View();
        }


        //Đọc danh sách Cartitem từ session 
        List<Cartitem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("shopcart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cartitem>>(jsoncart);
            }
            return new List<Cartitem>();
        }

        // Lưu danh sách CartItem trong giỏ hàng vào session
        void SaveCartSession(List<Cartitem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("shopcart", jsoncart);
        }

        // Xóa session giỏ hàng
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("shopcart");
        }
        // Cho hàng vào giỏ
        public async Task<IActionResult> AddToCart(int id)
        {
            var mathang = await _context.Mathang
                .FirstOrDefaultAsync(m => m.MaMh == id);
            if (mathang == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }
            var cart = GetCartItems();
            var item = cart.Find(p => p.Mathang.MaMh == id);
            if (item != null)
            {
                item.Soluong++;
            }
            else
            {
                cart.Add(new Cartitem() { Mathang = mathang, Soluong = 1 });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }
        // Chuyển đến view xem giỏ hàng
        public IActionResult ViewCart()
        {
            GetInfo();
            return View(GetCartItems());
        }
        public IActionResult RemoveItem(int id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Mathang.MaMh == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        // Cập nhật số lượng một mặt hàng trong giỏ
        public IActionResult UpdateItem(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Mathang.MaMh == id);
            if (item != null)
            {
                item.Soluong = quantity;
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        // Chuyển đến view thanh toán
        public IActionResult CheckOut()
        {
            GetInfo();
            return View(GetCartItems());
        }



        [HttpPost, ActionName("CreateBill")]
        // Lưu thông tin đơn hàng
        public async Task<IActionResult> CreateBill(int id, string email, string hoten, string dienthoai, string diachi)
        {

            // Xử lý thông tin khách hàng trường hợp khách hàng mới
            var kh = new Khachhang();
            if (id != 0) // khách hàng đã đăng nhập
            {
                kh.MaKh = id;
            }
            else
            {
                kh.Ten = hoten;
                kh.Email = email;
                kh.DienThoai = dienthoai;
                _context.Add(kh);
                await _context.SaveChangesAsync();
            }

            var hd = new Hoadon();
            hd.Ngay = DateTime.Now;
            hd.MaKh = kh.MaKh;
            _context.Add(hd);
            await _context.SaveChangesAsync();


            //Thêm chi tiết hóa đơn
            var cart = GetCartItems();
            int sl = 0;
            int thanhtien = 0;
            int tongtien = 0;
            foreach (var i in cart)
            {
                var ct = new Cthoadon();
                ct.MaHd = hd.MaHd;
                ct.MaMh = i.Mathang.MaMh;
                thanhtien = i.Mathang.GiaBan * i.Soluong;
                tongtien += thanhtien;
                ct.DonGia = i.Mathang.GiaBan;
                ct.SoLuong = (short)i.Soluong;
                ct.ThanhTien = thanhtien;
                _context.Add(ct);
                Mathang mh = _context.Mathang.FirstOrDefault(k => k.MaMh == i.Mathang.MaMh);
               
                //Điều chỉnh số lượng và lượt mua
                if (mh != null)
                {
                    sl = (int)(mh.SoLuong - (short)i.Soluong);
                    mh.SoLuong = (short)sl;
                    mh.LuotMua += i.Soluong;
                    _context.Mathang.Update(mh);
                }
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();

            ///Cập nhật
            hd.TongTien = tongtien;
            _context.Update(hd);
            await _context.SaveChangesAsync();

            //Xóa giỏ hàng
            ClearCart();
            GetInfo();
            return View(hd);
        }

        public async Task<IActionResult> ctdh(int id, string email, string hoten, string dienthoai, string diachi)
        {

            // Xử lý thông tin khách hàng trường hợp khách hàng mới
            var kh = new Khachhang();
            if (id != 0) // khách hàng đã đăng nhập
            {
                kh.MaKh = id;
            }
            else
            {
                kh.Ten = hoten;
                kh.Email = email;
                kh.DienThoai = dienthoai;
                _context.Add(kh);
                await _context.SaveChangesAsync();
            }

            var hd = new Hoadon();
            hd.Ngay = DateTime.Now;
            hd.MaKh = kh.MaKh;
            _context.Add(hd);
            await _context.SaveChangesAsync();


            //Thêm chi tiết hóa đơn
            var cart = GetCartItems();
            int sl = 0;
            int thanhtien = 0;
            int tongtien = 0;
            foreach (var i in cart)
            {
                var ct = new Cthoadon();
                ct.MaHd = hd.MaHd;
                ct.MaMh = i.Mathang.MaMh;
                thanhtien = i.Mathang.GiaBan * i.Soluong;
                tongtien += thanhtien;
                ct.DonGia = i.Mathang.GiaBan;
                ct.SoLuong = (short)i.Soluong;
                ct.ThanhTien = thanhtien;
                _context.Add(ct);
                Mathang mh = _context.Mathang.FirstOrDefault(k => k.MaMh == i.Mathang.MaMh);
                //Điều chỉnh số lượng và lượt mua
                if (mh != null)
                {
                    sl = (int)(mh.SoLuong - (short)i.Soluong);
                    mh.SoLuong = (short)sl;
                    mh.LuotMua += i.Soluong;
                    _context.Mathang.Update(mh);
                }
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();

            ///Cập nhật
            hd.TongTien = tongtien;
            _context.Update(hd);
            await _context.SaveChangesAsync();

            //Xóa giỏ hàng
            ClearCart();
            GetInfo();
            return View(hd);
        }

        //Đăng nhập
        public IActionResult Login()
        {
            GetInfo();
            return View();
        }
        public IActionResult Login1()
        {
            GetInfo();
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string matkhau)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matkhau))
            {
                return RedirectToAction(nameof(Login1));
            }
            var nv1 = _context.Nhanvien.FirstOrDefault(k => k.Email == email);
            var nv2 = _context.Nhanvien.FirstOrDefault(k => k.MatKhau == matkhau);
            var kh = _context.Khachhang.FirstOrDefault(k => k.Email == email);
            if (kh != null && _pwHear.VerifyHashedPassword(kh, kh.MatKhau, matkhau) == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetString("khachhang", kh.Email);
                return RedirectToAction(nameof(Customer));
            }
            else
            {
                if (nv1 != null && nv2 != null)
                {
                    HttpContext.Session.SetString("Nhanvien", nv1.Email);
                    return RedirectToAction("Index", "Mathangs");
                }
            }
            return RedirectToAction(nameof(Login1));
        }

        public IActionResult Customer()
        {
            GetInfo();
            return View();
        }

        public IActionResult Register()
        {
            GetInfo();
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string matkhau, string hoten, string dienthoai)
        {
            // kiểm tra email đã tồn tại 


            // thêm khach hàng vào db
            var kh = new Khachhang();
            kh.Email = email;
            kh.MatKhau = _pwHear.HashPassword(kh, matkhau);   // mã hóa mật khẩu
            kh.Ten = hoten;
            kh.DienThoai = dienthoai;
            _context.Add(kh);
            _context.SaveChanges();


            return RedirectToAction(nameof(Login));
        }

        //Đăng xuất
        public IActionResult Signout()
        {
            HttpContext.Session.SetString("khachhang", "");
            HttpContext.Session.SetString("Nhanvien", "");
            GetInfo();
            return RedirectToAction(nameof(Index));
        }

        //Tìm kiếm
        public async Task<IActionResult> Search(string SearchKey)
        {
            var lstHang = await _context.Mathang.Include(m => m.MaDmNavigation)
                            .Where(k => k.Ten.Contains(SearchKey) && k.GiaBan >= 0).ToListAsync();
            GetInfo();
            return View(lstHang);
        }

        //Khách hàng xem chi tiết mua hàng
        public IActionResult xemcthdKhachhang(int id)
        {
            var applicationDbContext = _context.Hoadon.Include(m => m.MaKhNavigation).Where(d => d.TrangThai == 0 && d.MaKh == id);
            GetInfo();
            return View(applicationDbContext);
        }
        public IActionResult chitiet(int id)
        {
            var lstCTHoaDon = _context.Cthoadon.Include(c => c.MaMhNavigation).Where(c => c.MaHd == id);
            GetInfo();
            return View(lstCTHoaDon);
        }


        //Khách hàng tự thay đổi mật khẩu
        public IActionResult Thaydoimatkhau()
        {
            GetInfo();
            return View();
        }

        [HttpPost]
        public IActionResult Thaydoimatkhau(Khachhang model)
        {
            var customerId = User.Identity.Name;
            var customer = _context.Khachhang.FirstOrDefault(k => k.Email == customerId);

            if (model != null && customer != null && !string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(customer.MatKhau))
            {
                if (!VerifyPassword(model.CurrentPassword, customer.MatKhau))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu hiện tại không đúng.");
                    return View(model);
                }

                // Kiểm tra tính hợp lệ của mật khẩu mới
                if (!IsPasswordValid(model.NewPassword))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu mới không đáp ứng yêu cầu.");
                    return View(model);
                }

                // Lưu mật khẩu mới vào cơ sở dữ liệu
                customer.MatKhau = HashPassword(model.NewPassword);
                _context.SaveChanges();

                //thông báo thành công
                TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi thành công.";
                return RedirectToAction("Customer", "Mathangs");
            }
            else
            {
                // Xử lý khi một trong hai mật khẩu không hợp lệ
                if (model == null)
                {
                    ModelState.AddModelError(string.Empty, "Model không hợp lệ.");
                }
                else if (customer == null)
                {
                    ModelState.AddModelError(string.Empty, "Khách hàng không tồn tại.");
                }
                else
                {
                    if (string.IsNullOrEmpty(model.CurrentPassword))
                    {
                        ModelState.AddModelError(string.Empty, "Vui lòng nhập mật khẩu hiện tại.");
                    }
                    else if (string.IsNullOrEmpty(customer.MatKhau))
                    {
                        ModelState.AddModelError(string.Empty, "Mật khẩu đã lưu không tồn tại.");
                    }
                }
            }

            return View(model);
        }
        private bool VerifyPassword(string enteredPassword, string savedPassword)
        {
            //So sánh hai chuỗi mật khẩu
            return enteredPassword == savedPassword;
        }

        private bool IsPasswordValid(string newPassword)
        {
            //Kiểm tra độ dài của mật khẩu
            return newPassword.Length >= 3; // Mật khẩu phải có ít nhất 3 ký tự
        }

        private string HashPassword(string password)
        {
            //Sử dụng một thuật toán băm như SHA256
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
