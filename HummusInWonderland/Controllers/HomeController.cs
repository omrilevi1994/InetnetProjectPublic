using HummusInWonderland.DAL;
using HummusInWonderland.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HummusInWonderland.Controllers
{
    public class HomeController : Controller
    {
        private HummhusInWonderlandContext db = new HummhusInWonderlandContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            String message = "מחפשים חומוס טרי ואיכותי בעל מרקם עדין וטעם עשיר? הגעתם למקום הנכון!\n חומוב פלא היא מסעדת שף המתמחה בחומוס טרי ללא חומרים משמרים. החומוס נטחן במקום ומוגש לכם חם, טרי ורענן, על מנת להבטיח עבורכם חוויה קולינרית נפלאה.\n השפים דניאל, עליסה ועמית הנם בעלי ניסיון עשיר וידע רב בכל עולמות האוכל, הדבר בא לידי ביטוי במנות המוגשות במקום. חומוס-פלא ממוקם באזורים מרכזיים בארץ - באר שבע, חיפה, תל אביב וירושלים. מיד עם פתיחתו הפך המקום לאבן שואבת לכל חסידי החומוס האיכותי והטרי. כיום, לקוחות מגיעים למקום מכל יישובי המרכז ומכל רחבי הארץ בגלל השם והמוניטין, במקום 30 מקומות ישיבה לסועדים וישנו שירות משלוחים.";
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