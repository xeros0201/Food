using AutoMapper;
using Food.Data;
using Microsoft.EntityFrameworkCore;
using ReviewFood.Models;
using Microsoft.AspNetCore.Mvc;

namespace ReviewFood.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext db;
   
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            db = context;
        
            _hostEnvironment = hostEnvironment;
        }
        public ActionResult Index()
        {
            var featured = db.BaiViets.Include( c => c.DanhMuc).Take(3);
            ViewBag.Featured = featured;

            ViewBag.TopTen = db.BaiViets.Include(c => c.DanhMuc).Take(10);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [NonAction]
        public ActionResult MenuView()
        {
            var menu = db.DanhMucChas.Include( c => c.DanhMucs).ToList();
            return PartialView(menu);
        }
    }
}