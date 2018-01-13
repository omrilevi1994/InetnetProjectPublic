using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using SushiFactory.Models;
using System.Data.Entity.Migrations;

namespace SushiFactory.DAL
{
    public class SushiInitializer : System.Data.Entity.DropCreateDatabaseAlways<SushiFactoryContext>
    {
        protected override void Seed(SushiFactoryContext context)
        {
            var menu = new List<Product>
            {
                new Product { ProductID = 1,   ProductName = "הקוקטייל",
                    ProductDescription = "אספרגוס טמפורה, אבוקדו, צנון מוחמץ, בצל ירוק, שומשום, שקדים קלויים וטריאקי",
                Price = 38, ProductImage = "/Images/the_Cocktail.jpg", Vegi = false},
                new Product { ProductID = 2,   ProductName = "הצמחוני",
                    ProductDescription = "פטריות שיטאקה, אבוקדו, מלפפון, גזר, בצל ירוק, ספייסי מיונז וטריאקי",
                Price = 34, ProductImage = "/Images/the_vegan.jpg", Vegi = true},
                new Product { ProductID = 3,   ProductName = "המעורבב",
                    ProductDescription = "מיקס דגים לבנים, אבוקדו, שקדים קלויים, בצל ירוק וספייסי מיונז",
                Price = 34, ProductImage = "/Images/the_mix.jpg", Vegi = false},
                new Product { ProductID = 4,   ProductName = "הפירותי",
                    ProductDescription = "שרימפ, סקלופ, קלמארי, ביצי סלמון, בצל ירוק, ספייסי מיונז וטריאקי",
                Price = 25, ProductImage = "/Images/the_fruity.jpg", Vegi = false},
                new Product { ProductID = 5,   ProductName = "הצלופח",
                    ProductDescription = "צלופח מבושל בטריאקי, אבוקדו, אושינקו, שקדים קלויים, בצל ירוק ושומשום לבן",
                Price = 25,ProductImage = "/Images/the_eel.jpg", Vegi = false},
            };

            menu.ForEach(m => context.Menu.AddOrUpdate(p => p.ProductID, m));
            context.SaveChanges();

            var customer = new List<Customer>
            {
                new Customer {CustomerID = 1, FirstName = "דוד", LastName ="סאפירו", BirthDate = DateTime.Parse("1995-08-09") ,
                Gender = Gender.male, Email = "david.sapiro@gmail.com", City ="תל אביב", Street = "שפרינצק", Password ="1234567", PhoneNumber = "0526500607"},
                new Customer {CustomerID = 2, FirstName = "עמרי", LastName ="לוי", BirthDate = DateTime.Parse("1994-11-17") ,
                Gender = Gender.male, Email = "omrilevi1994@gmail.com", City ="ראשון לציון", Street = "צבי פרנק", Password ="1234567", PhoneNumber = "0526624476"},
                new Customer {CustomerID = 3, FirstName = "ליטל", LastName ="נסרי", BirthDate = DateTime.Parse("1994-03-11") ,
                Gender = Gender.female, Email = "litalnasri@gmail.com", City ="תל אביב", Street = "אבן גבירול", Password ="1234567", PhoneNumber = "0546177891"},
            };
            customer.ForEach(c => context.Customers.AddOrUpdate(p => p.CustomerID, c));
            context.SaveChanges();

            var branch = new List<Branch>
            {
                new Branch {BranchID = 1, BranchCity= "תל אביב", BranchName = "סושי פקטורי", BranchsHouseNumber = 1,BranchsPhoneNumber = "03-4678953", BranchStreet = "אבן גבירול" , CoordX = 32.076237, CoordY = 34.781488},
                new Branch {BranchID = 2, BranchCity= "ירושלים", BranchName = "סושי פקטורי", BranchsHouseNumber = 2,BranchsPhoneNumber = "04-7895034", BranchStreet = "האגוז", CoordX = 31.784320, CoordY = 35.212737},
                new Branch {BranchID = 3, BranchCity= "ראשון לציון", BranchName = "סושי פקטורי", BranchsHouseNumber = 3,BranchsPhoneNumber = "03-6457890", BranchStreet = "רוטשילד", CoordX = 31.964140, CoordY = 34.796997},
                new Branch {BranchID = 4, BranchCity= "חיפה", BranchName = "סושי פקטורי", BranchsHouseNumber = 4,BranchsPhoneNumber = "09-8765942", BranchStreet = "הנמל", CoordX = 32.819191, CoordY = 35.000484}
            };
            branch.ForEach(b => context.Branches.AddOrUpdate(p => p.BranchID, b));
            context.SaveChanges();

            var orders = new List<Order>
            {
                new Order { OrderID = 1, CustomerId = customer.Single(s => s.LastName == "סאפירו").CustomerID, OrderDate = DateTime.Parse("2016-02-14"), BranchID = branch.Single(b => b.BranchCity == "ראשון לציון").BranchID},
                new Order { OrderID = 2, CustomerId = customer.Single(s => s.LastName == "סאפירו").CustomerID, OrderDate = DateTime.Parse("2016-03-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 3, CustomerId = customer.Single(s => s.LastName == "לוי").CustomerID,  OrderDate = DateTime.Parse("2016-04-14"), BranchID = branch.Single(b => b.BranchCity == "חיפה").BranchID},
                new Order { OrderID = 4, CustomerId = customer.Single(s => s.LastName == "לוי").CustomerID, OrderDate = DateTime.Parse("2016-05-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 5, CustomerId = customer.Single(s => s.LastName == "נסרי").CustomerID, OrderDate = DateTime.Parse("2016-05-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 6, CustomerId = customer.Single(s => s.LastName == "נסרי").CustomerID, OrderDate = DateTime.Parse("2016-06-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 7, CustomerId = customer.Single(s => s.LastName == "נסרי").CustomerID, OrderDate = DateTime.Parse("2016-06-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID}

            };
            orders.ForEach(o => {
                o.Products.Add(menu.Single(s => s.ProductName == "הקוקטייל"));
                o.Products.Add(menu.Single(s => s.ProductName == "הצלופח"));
                context.Orders.Add(o);
            });

            context.SaveChanges();
        }
    }
}