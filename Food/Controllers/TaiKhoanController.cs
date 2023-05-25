using Food.Data;
using Microsoft.AspNetCore.Mvc;
using ReviewFood.Models;

namespace Food.Controllers
{
    public class TaiKhoanController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ISession Session { get; }

        public TaiKhoanController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            db = context;

            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            Session = _httpContextAccessor.HttpContext.Session;
        }
        
        // GET: TaiKhoan
        public ActionResult Login()
            {
                if ( Session.GetString("TaiKhoan") == null)
                {
                    return View();
                }
                else
                    return Redirect("/");
            }

            [HttpPost]
            public ActionResult Login(string TenDangNhap, string MatKhau)
            {
                var data = db.TaiKhoan.Where(tk => tk.TenDangNhap == TenDangNhap && tk.MatKhau == MatKhau).FirstOrDefault();
                if (data == null)
                {
                    ViewBag.Error = "Thông tin đăng nhập không đúng";
                    return View();
                }
                if (data.Quyen == true)
                {
                    ViewBag.Done = "Chào mừng Admin";
                    Session.SetString("TaiKhoan", data.TenDangNhap + "," + data.HoTen + "," + data.Id + ",true");
                    return RedirectToAction("Index", "Admin/Home");
                }
                else
                {
                    ViewBag.Done = "Đăng nhập thành công";
                    string data_account = data.TenDangNhap + "," + data.HoTen + "," + data.Id + ",";
                //Session["TaiKhoan"] = data;
                    Session.SetString("TaiKhoan", data_account);
                    return Redirect("/");
                }
            }
            public ActionResult Logout()
            {
                Session.Remove("TaiKhoan");
                return Redirect("/");
                //Session.Abandon();
                //return RedirectToAction("Login", "TaiKhoan");
            }

            public ActionResult AdminToHome()
            {
                return Redirect("/");
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Create(TaiKhoan collection)
            {
                try
                {
                    TaiKhoan taiKhoan = new TaiKhoan();
                    taiKhoan.HoTen = collection.HoTen;
                    taiKhoan.GioiTinh = collection.GioiTinh;
                    taiKhoan.TenDangNhap = collection.TenDangNhap;
                    taiKhoan.MatKhau = collection.MatKhau;
                    taiKhoan.Email = collection.Email;
                    taiKhoan.DiaChi = collection.DiaChi;
                    taiKhoan.TrangThai = collection.TrangThai;
                    taiKhoan.Quyen = false;
                    taiKhoan.NgaySinh =  collection.NgaySinh;
                    db.TaiKhoan.Add(taiKhoan);
                    db.SaveChanges();
                    ViewBag.Done = "Đăng ký thành công";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }

            [HttpGet]
            public ActionResult Edit(int id)
            {
                if (Session.GetString("TaiKhoan") == null)
                {
                    return Redirect("/");
                }
                else
                {
                    string data = Session.GetString("TaiKhoan").ToString();
                    string[] Account = new string[3];
                    Account = (data != null) ? data.Split(',') : Account;
                    if (int.Parse(Account[2]) != id)
                    {
                        return Redirect("/");
                    }
                }
                return View(db.TaiKhoan.Find(id));
            }

            [HttpPost]
            public ActionResult Edit(TaiKhoan taiKhoans)
            {
                try
                {
                    var tk = db.TaiKhoan.Find(taiKhoans.Id);
                    tk.HoTen = taiKhoans.HoTen;
                    tk.MatKhau = taiKhoans.MatKhau;
                    tk.GioiTinh = taiKhoans.GioiTinh;
                    tk.Email = taiKhoans.Email;
                    tk.DiaChi = taiKhoans.DiaChi;
                    tk.NgaySinh = taiKhoans.NgaySinh;
                    db.SaveChanges();
                    ViewBag.Done = "Sửa tài khoản thành công";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View(taiKhoans);
            }
            public ActionResult Detail(int id)
            {
                var thongtin = db.TaiKhoan.Where(m => m.Id == id).First();
                return View(thongtin);
            }
        }
    }
