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
    public class OrdersController : Controller
    {
        private HummhusInWonderlandContext db = new HummhusInWonderlandContext();

        // GET: Orders
        public ActionResult Index()
        {
            ViewBag.branches = new SelectList(db.Branches, "BranchID", "DisplayName");
            ViewBag.customers = new SelectList(db.Customers, "CustomerID", "DisplayName");
            return View(db.Orders.ToList());
        }

        [HttpPost]
        public ActionResult Index(int? branchId, int? customerId)
        {
            ViewBag.branches = new SelectList(db.Branches, "BranchID", "DisplayName");
            ViewBag.customers = new SelectList(db.Customers, "CustomerID", "DisplayName");

            var orders = from o in db.Orders select o;

            if (branchId != null)
            {
                orders = orders.Where(o => o.BranchID == branchId);
            }
            if (customerId != null)
            {
                orders = orders.Where(o => o.CustomerId == customerId);
            }

            return View(orders.ToList());
        }

        // GET: Orders
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order= db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Order/Edit/:id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.products = db.Menu.ToList();
            return View(order);
        }

        // POST
        [HttpPost]
        public JsonResult RemoveFromOrder(int orderId, int productId)
        {
            Order order = db.Orders.Find(orderId);
            if (order == null)
            {
                return Json(false);
            }
            Product prod = order.Products.FirstOrDefault(p => p.ProductID == productId);

            if (prod == null)
            {
                return Json(false);
            }

            order.Products.Remove(prod);
            db.SaveChanges();
            return Json(true);
        }

        [HttpPost]
        public JsonResult AddProductToOrder(int orderId, int productId)
        {
            Order order = db.Orders.Find(orderId);
            if (order == null)
            {
                return Json(false);
            }
            Product prod = order.Products.FirstOrDefault(p => p.ProductID == productId);

            if (prod != null)
            {
                return Json(false);
            }
            prod = db.Menu.FirstOrDefault(p => p.ProductID == productId);
            if (prod == null)
            {
                return Json(false);
            }

            order.Products.Add(prod);
            db.SaveChanges();
            return Json(new {Price= prod.Price, ProductName = prod.ProductName});
        }

        public ActionResult ShoppingCart()
        {
            List<Product> order = new List<Product>();

            int total = 0;

            if (System.Web.HttpContext.Current.Session["shoppingCart"] != null)
            {
                foreach (var item in (List<int>)System.Web.HttpContext.Current.Session["shoppingCart"])
                {
                    var product = db.Menu.Where(a => a.ProductID == item).FirstOrDefault();
                    if (product != null)
                    {
                        order.Add(product);
                        total += product.Price;
                    }
                }
            }

            ViewBag.Total = total;
            ViewBag.branches = db.Branches.ToList();
            return View(order);
        }

        [HttpPost]
        public JsonResult AddToCart(int productID)
        {
            List<int> shoppingList = (List<int>)System.Web.HttpContext.Current.Session["shoppingCart"];

            if (shoppingList == null)
            {
                shoppingList = new List<int>();
                System.Web.HttpContext.Current.Session["shoppingCart"] = shoppingList;
            }
            if (!shoppingList.Contains(productID))
                shoppingList.Add(productID);
            return Json(true);
        }

        public JsonResult DeleteFromCart(int productID = 0)
        {
            List<int> cart = (List<int>)System.Web.HttpContext.Current.Session["shoppingCart"];
            if (cart != null)
            {
                cart.Remove(productID);
                return Json(cart.Count);
            } else
                return Json(0);
        }

        public JsonResult Pay(int branchId = 1)
        {
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                return Json(false);
            }
            else
            {
                Order order = new Order
                {
                    CustomerId = ((Customer)System.Web.HttpContext.Current.Session["user"]).CustomerID,
                    BranchID = branchId,
                    OrderDate = DateTime.Now,
                    Products = new List<Product>()
                };

                foreach (var item in (List<int>)System.Web.HttpContext.Current.Session["shoppingCart"])
                {
                    order.Products.Add(db.Menu.Single(p => p.ProductID == item));
                }

                db.Orders.Add(order);
                db.Customers.Find(order.CustomerId).Orders.Add(order);

                db.SaveChanges();
                ((List<int>)System.Web.HttpContext.Current.Session["shoppingCart"]).Clear();

                return Json(true);
            }
        }

        public ActionResult OrdersByBranch(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var query = from o in db.Orders
                        join b in db.Branches on
                        o.BranchID equals b.BranchID
                        where b.BranchID == id
                        select new BranchOrdersView
                        {
                            branchId = o.BranchID,
                            branchName = b.BranchName,
                            branchCity = b.BranchCity,
                            productNames = o.Products.ToString(),
                            orderDate = o.OrderDate
                        };

            return View(query);
        }

        public ActionResult GroupByYear()
        {
            // select the doch
            var groupResult = db.Orders.GroupBy(b => b.OrderDate.Year).Select(g => new OrderYearsViewModel { year = g.Key, postCount = g.Count() });
            return View(groupResult);
        }
    }
}
