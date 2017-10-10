using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using HummusInWonderland.Models;
using System.Data.Entity.Migrations;

namespace HummusInWonderland.DAL
{
    public class HummusInitializer : System.Data.Entity.DropCreateDatabaseAlways<HummhusInWonderlandContext>
    {
        protected override void Seed(HummhusInWonderlandContext context)
        {
 
            var menu = new List<Product>
            {
                new Product { ProductID = 1,   ProductName = "חומוס בשר",
                    ProductDescription = "חומוס עם בשר בקר טחון, גרגרים, פטרוזיליה, שמן זית, כמון, פפריקה, 2פיתות, חמוצים",
                Price = 38, ProductImage = "/Images/hummus_meat_top.png", Vegi = false},
                new Product { ProductID = 2,   ProductName = "חומוס פטריות",
                    ProductDescription = "חומוס עם פטריות שלמות, מבושלות עם בצל ותבלינים. מוגש עם גרגרים, פטרוזיליה, שמן זית, כמון, פפריקה, 2פיתות, חמוצים",
                Price = 34, ProductImage = "/Images/hummus_mushroom_top.png", Vegi = true},
                new Product { ProductID = 3,   ProductName = "חומוס צנובר",
                    ProductDescription = "חומוס עם צנובר קלוי בתנור, מוגש עם גרגרים, פטרוזיליה, שמן זית, כמון, פפריקה, 2פיתות, חמוצים",
                Price = 34, ProductImage = "/Images/hummus_tznubar_top.png", Vegi = true},
                new Product { ProductID = 4,   ProductName = "חומוס",
                    ProductDescription = "חומוס עם גרגרים, פטרוזיליה, שמן זית, ביצה, כמון, פפריקה, 2פיתות, חמוצים",
                Price = 25, ProductImage = "/Images/hummus_top.png", Vegi = true},
                new Product { ProductID = 5,   ProductName = "חומוס פול",
                    ProductDescription = "חומוס עם פול גרגרים, פטרוזיליה, שמן זית, ביצה, כמון, פפריקה, , 2פיתות, חמוצים",
                Price = 25,ProductImage = "/Images/hummus_ful_top.png", Vegi = true},
                new Product { ProductID = 6,   ProductName = "חומוס משאוושה",
                    ProductDescription = "משוואשה על חומוס מוגשת עם גרגרים, פטרוזיליה, שמן זית, ביצה, כמון, פפריקה, 2פיתות, חמוצים",
                Price = 25,ProductImage = "/Images/mashausha_top.png", Vegi = true},
                new Product { ProductID = 7,   ProductName = "המשולשת",
                    ProductDescription = "1/3חומוס 1/3משוואשה 1/3פול, מוגשת עם גרגרים, פטרוזיליה, שמן זית, ביצה, כמון, פפריקה, 2פיתות, חמוצים",
                Price = 25,ProductImage = "/Images/hummus_tri_top.png", Vegi = true},
                new Product { ProductID = 8,   ProductName = "מנה סלט",
                    ProductDescription = "מנת סלט עם חומוס, טחינה, גרגרים, וביצה",
                Price = 25,ProductImage = "/hummus_salad_front.png", Vegi = true},

            };
            menu.ForEach(m => context.Menu.AddOrUpdate(p => p.ProductID, m));
            context.SaveChanges();

            var customer = new List<Customer>
            {
                new Customer {CustomerID = 1, FirstName = "דניאל", LastName ="יעקובסן", BirthDate = DateTime.Parse("1994-08-13") ,
                Gender = Gender.male, Email = "halleldaniel@gmail.com", City ="ראש העין", Street = "חבצלת", Password ="1234567", PhoneNumber = "0543034020"},
                new Customer {CustomerID = 2, FirstName = "עמית", LastName ="הררי", BirthDate = DateTime.Parse("1993-11-19") ,
                Gender = Gender.male, Email = "amit.harari93@gmail.com", City ="תל אביב", Street = "אבן גבירול", Password ="1234567", PhoneNumber = "0528465750"},
                new Customer {CustomerID = 3, FirstName = "עליסה", LastName ="קריבורוצקי", BirthDate = DateTime.Parse("1994-03-11") ,
                Gender = Gender.female, Email = "alisa6450@gmail.com", City ="באר שבע", Street = "גלזמן", Password ="1234567", PhoneNumber = "0543016142"},
                new Customer {CustomerID = 4, FirstName = "שחר", LastName ="חסון", BirthDate = DateTime.Parse("1980-09-17") ,
                Gender = Gender.male, Email = "shaharhason@gmail.com", City ="תימן", Street = "תימנים", Password ="1234567", PhoneNumber = "0507878787"},
                new Customer {CustomerID = 5, FirstName = "אדיר", LastName ="מילר", BirthDate = DateTime.Parse("1983-04-12") ,
                Gender = Gender.male, Email = "adirmiller@gmail.com", City ="פתח תקווה", Street = "כלנית", Password ="1234567", PhoneNumber = "0526767489"},
                new Customer {CustomerID = 6, FirstName = "בר", LastName ="רפאלי", BirthDate = DateTime.Parse("1985-09-13"),
                Gender = Gender.female, Email = "barrefaeli@gmail.com", City ="תל אביב", Street = "הדוגמנויות", Password ="1234567", PhoneNumber = "0526839072"},
                new Customer {CustomerID = 7, FirstName = "גל", LastName ="גדות", BirthDate = DateTime.Parse("1986-02-18") ,
                Gender = Gender.female, Email = "galgadot@gmail.com", City ="תל אביב", Street = "הדוגמנויות", Password ="1234567", PhoneNumber = "0508362973"},
                new Customer {CustomerID = 7, FirstName = "admin", LastName ="admin", BirthDate = DateTime.Parse("1986-02-18") ,
                Gender = Gender.female, Email = "admin@gmail.com", City ="תל אביב", Street = "הדוגמנויות", Password ="111111", PhoneNumber = "0508362873"},
            };
            customer.ForEach(c => context.Customers.AddOrUpdate(p => p.CustomerID, c));
            context.SaveChanges();

            var branch = new List<Branch>
            {
                new Branch {BranchID = 1, BranchCity= "תל אביב", BranchName = "חומוס פלא", BranchsHouseNumber = 7,BranchsPhoneNumber = "03-4678953", BranchStreet = "קרליבך" , CoordX = 32.066883, CoordY = 34.783395},
                new Branch {BranchID = 2, BranchCity= "ירושלים", BranchName = "חומוס פלא", BranchsHouseNumber = 12,BranchsPhoneNumber = "04-7895034", BranchStreet = "שלמה המלך", CoordX = 31.778688, CoordY = 35.223516},
                new Branch {BranchID = 3, BranchCity= "באר שבע", BranchName = "חומוס פלא", BranchsHouseNumber = 56,BranchsPhoneNumber = "08-6457890", BranchStreet = "אברהם אבינו", CoordX = 31.270047, CoordY = 34.794354},
                new Branch {BranchID = 4, BranchCity= "חיפה", BranchName = "חומוס פלא", BranchsHouseNumber = 3,BranchsPhoneNumber = "09-8765942", BranchStreet = "הנשיא", CoordX = 32.816080, CoordY = 34.979221}
            };
            branch.ForEach(b => context.Branches.AddOrUpdate(p => p.BranchID, b));
            context.SaveChanges();

            var orders = new List<Order>
            {
                new Order { OrderID = 1, CustomerId = customer.Single(s => s.LastName == "יעקובסן").CustomerID, OrderDate = DateTime.Parse("2016-02-14"), BranchID = branch.Single(b => b.BranchCity == "באר שבע").BranchID},
                new Order { OrderID = 2, CustomerId = customer.Single(s => s.LastName == "יעקובסן").CustomerID, OrderDate = DateTime.Parse("2016-03-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 3, CustomerId = customer.Single(s => s.LastName == "רפאלי").CustomerID,  OrderDate = DateTime.Parse("2016-04-14"), BranchID = branch.Single(b => b.BranchCity == "חיפה").BranchID},
                new Order { OrderID = 4, CustomerId = customer.Single(s => s.LastName == "הררי").CustomerID, OrderDate = DateTime.Parse("2016-05-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 5, CustomerId = customer.Single(s => s.LastName == "קריבורוצקי").CustomerID, OrderDate = DateTime.Parse("2016-05-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 6, CustomerId = customer.Single(s => s.LastName == "מילר").CustomerID, OrderDate = DateTime.Parse("2016-06-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID},
                new Order { OrderID = 7, CustomerId = customer.Single(s => s.LastName == "מילר").CustomerID, OrderDate = DateTime.Parse("2016-06-14"), BranchID = branch.Single(b => b.BranchCity == "תל אביב").BranchID}

            };
            orders.ForEach(o => {
                o.Products.Add(menu.Single(s => s.ProductName == "חומוס"));
                o.Products.Add(menu.Single(s => s.ProductName == "חומוס פטריות"));
                context.Orders.Add(o);
            });

            context.SaveChanges();
        }
    }
}