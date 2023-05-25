using ReviewFood.Models;
using Microsoft.AspNetCore.Mvc;
using Food.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Grpc.Core;

namespace ReviewFood.Controllers
{
    public class BaiVietController : Controller
    {
     
        // GET: BaiViet

        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public BaiVietController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment )
        {
            db = context;
       
            _hostEnvironment = hostEnvironment;
        }
        public ActionResult Index(int id = 0)
        {
            if (TempData["Done"] != null || TempData["Error"] != null)
            {
                ViewBag.Done = TempData["Done"];
                ViewBag.Error = TempData["Error"];
                TempData.Remove("Done");
                TempData.Remove("Error");
            }
            var data = db.BaiViets.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            object comments = db.DanhGias.Where(d => d.Id == id).ToList();
            ViewBag.Comments = comments;
            return View(data);
        }

        public ActionResult Search(string keywork = "", int page = 1)
        {
            var TinTucs = db.BaiViets.Where(d => d.TieuDe.Contains(keywork) || d.NoiDung.Contains(keywork)).ToPagedList(page,12);
            return View(TinTucs);
        }
        public ActionResult Create()
        {
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            return View();
        }

        // POST: Admin/BaiViet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaiViet baiViet, IFormFile img)
        {
            try
            {
                string filename = "";
                if (img != null)
                {
                    filename = Path.GetFileNameWithoutExtension(img.FileName);
                }

                else
                {
                    filename = "";
                }
                if (filename != "")
                {
                    string extension = Path.GetExtension(img.FileName);
                    if (extensionFile(extension) == true)
                    {
                        string fileId = Guid.NewGuid().ToString().Replace("-", "");
       

                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Assets", "Image");
                        string uniqueFileName = fileId + extension;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                              img.CopyTo(stream);
                        }
                        baiViet.HinhAnh = uniqueFileName;
                    }
                    else
                    {
                        ViewBag.Error = "File không đúng định dạng, hãy điền lại dữ liệu";
                        return View(baiViet);
                    }
                }
                baiViet.TrangThai = false;
                //baiViet.IdTaiKhoan = db.BaiViets.Max(p => p.IdTaiKhoan) + 1;
                baiViet.NgayTao = DateTime.Now;
                baiViet.NgaySua = DateTime.Now;
                db.BaiViets.Add(baiViet);
                db.SaveChanges();
                ViewBag.Done = "Thêm bài viết thành công, xin chờ duyệt";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            return View(baiViet);
        }
        public bool extensionFile(string extension)
        {
            var st_Exten = extension.ToLower();
            switch (st_Exten)
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".veg":
                    return true;
                default:
                    return false;
            }
        }
    }
}