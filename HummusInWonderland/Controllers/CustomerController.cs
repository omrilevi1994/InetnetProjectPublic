using HummusInWonderland.DAL;
using HummusInWonderland.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HummusInWonderland.Controllers
{
    public class CustomerController : Controller
    {
        private HummhusInWonderlandContext db = new HummhusInWonderlandContext();

        // GET: Customer
        public ActionResult Index()
        {
            ViewBag.City = new SelectList(db.Customers.Select(x => x.City).Distinct());

            var customers = db.Customers.Where(x => x.FirstName != "admin");

            return View(customers.ToList());
        }

        [HttpPost]
        public ActionResult Index(string FirstName, string LastName, string City, Gender gender)
        {
            var customers = from c in db.Customers select c;

            if (!string.IsNullOrEmpty(FirstName))
            {
                customers = customers.Where(x => x.FirstName == FirstName);
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                customers = customers.Where(x => x.LastName == LastName);
            }

            if (!string.IsNullOrEmpty(City))
            {
                customers = customers.Where(x => x.City == City);
            }

            customers = customers.Where(x => x.Gender == gender);


            ViewBag.City = new SelectList(db.Customers.Select(x => x.City).Distinct());
            return View(customers.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (db.Customers.Where(c => c.Email == customer.Email).Count() > 0)
                {
                    ViewBag.ErrMsg = "כתובת המייל שהוזנה כבר קיימת במערכת.";
                }
                else
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    System.Web.HttpContext.Current.Session["user"] = customer;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(customer);
        }



        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var existingUser = db.Customers.Where(s => s.Email == email &&
                                                    s.Password == password).SingleOrDefault();

            if (existingUser != null)
            {
                System.Web.HttpContext.Current.Session["user"] = existingUser;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMsg = "שם המשתמש או הסיסמא שהקשת, שגויים. אנא, נסה שוב";
            return View();
        }

        public ActionResult Logoff()
        {
            System.Web.HttpContext.Current.Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetFirstName(string term)
        {
            var firstNames = (from p in db.Customers where p.FirstName.Contains(term) select p.FirstName).Distinct().Take(10);

            return Json(firstNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLastName(string term)
        {
            var lastNames = (from p in db.Customers where p.LastName.Contains(term) select p.LastName).Distinct().Take(10);

            return Json(lastNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomersByBranch(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var query = from c in db.Customers
                        join b in db.Branches on
                        c.Orders.Select(x => x.Branch).Where(y => y.BranchID == id).FirstOrDefault().BranchID equals b.BranchID
                        where b.BranchID == id
                        select new BranchCustomerView
                        {
                            BranchId = b.BranchID,
                            branchName = b.BranchName,
                            branchCity = b.BranchCity,
                            firstName = c.FirstName,
                            lastName = c.LastName,
                            birthDate = c.BirthDate
                        };

            return View(query.ToList().Distinct());
        }
    }
}
