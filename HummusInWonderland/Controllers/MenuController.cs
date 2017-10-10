using HummusInWonderland.DAL;
using HummusInWonderland.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HummusInWonderland.Controllers
{
    public enum OPTIONS
    {
        כן,
        לא,
        שניהם
    };

    public class MenuController : Controller
    {
        private HummhusInWonderlandContext db = new HummhusInWonderlandContext();
        // GET: Menu
        public ActionResult Index()
        {
            ViewBag.MaxPrice = db.Menu.Select(x => x.Price).Max();
            return View(db.Menu.ToList());
        }

        [HttpPost]
        public ActionResult Index(OPTIONS? Vegi, OPTIONS? isPic, int? Price)
        {
            var menus = from a in db.Menu select a;

            if (Vegi != null)
            {
                if (OPTIONS.כן.Equals(Vegi))
                    menus = menus.Where(x => x.Vegi == true);
                else if (OPTIONS.לא.Equals(Vegi))
                    menus = menus.Where(x => x.Vegi == false);
            }

            if (isPic != null)
            {
                if (OPTIONS.כן.Equals(isPic))
                    menus = menus.Where(x => x.ProductImage != "/Images/No-Image.jpg");
                else if (OPTIONS.לא.Equals(isPic))
                    menus = menus.Where(x => x.ProductImage.ToLower().Equals("/Images/No-Image.jpg".ToLower()));
            }

            if (Price != null)
            {
                menus = menus.Where(x => x.Price <= Price);
            }

            ViewBag.MaxPrice = db.Menu.Select(x => x.Price).Max();
            return View(menus.ToList());
        }

        // GET: Menu/Details/5
        public ActionResult Details(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Menu.Find(productID);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            var physicalPath = "";
            var path = "";

            if (file == null)
            {
                physicalPath = "/Images/No-Image.jpg";
            }

            if (file != null && file.ContentLength > 0)
            {
                var FileName = string.Format("{0}.{1}", Guid.NewGuid(), "jpg");
                path = Path.Combine(Server.MapPath("~/Images"), FileName);

                Size szDimensions = new Size(98, 100);
                Bitmap resizedImg = new Bitmap(Image.FromStream(file.InputStream), szDimensions.Width, szDimensions.Height);

                physicalPath = "/Images/" + FileName;
                resizedImg.Save(path);
            }

            product.ProductImage = physicalPath;

            if (ModelState.IsValid)
            {
                db.Menu.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Menu.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.image = product.ProductImage;

            return View(product);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            var physicalPath = "";

            if (file != null && file.ContentLength > 0)
            {
                var fileName = string.Format("{0}.{1}", Guid.NewGuid(), "jpg");
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                Size szDimensions = new Size(340, 300);
                Bitmap resizedImg = new Bitmap(Image.FromStream(file.InputStream), szDimensions.Width, szDimensions.Height);

                physicalPath = "/Images/" + fileName;
                resizedImg.Save(path);

            }
            if (physicalPath != "")
            {
                product.ProductImage = physicalPath;
            }

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Menu.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int productId)
        {

            Product product = db.Menu.Find(productId);
            db.Menu.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
