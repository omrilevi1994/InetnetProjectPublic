using SushiFactory.DAL;
using SushiFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SushiFactory.Controllers
{
    public class HomeController : Controller
    {
        private SushiFactoryContext db = new SushiFactoryContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            String message = "סושי פקטורי הוא המקום בשבילכם !\nבמקום מוגשות מנות השף\nאנחנו מתחייבים לאיכות הגבוהה ביותר\nהסניפים שלנו פרוסים ברחבי הארץ";
            ViewBag.Message = message;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetBranches()
        {
            return Json(db.Branches.ToArray<Branch>());
        }
    }
}