using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HummusInWonderland.Models
{
    public class Order
    {
        public Order()
        {
            this.Products = new List<Product>();
        }
        public int OrderID { get; set; }

        [Required(ErrorMessage = "שדה חובה")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "שדה חובה")]
        public int BranchID { get; set; }

        [Display(Name = "תאריך הזמנה")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "מחיר כולל")]
        public int TotalPrice {
            get
            {
                var tot = 0;
                foreach (var p in this.Products)
                {
                    tot += p.Price;
                }
                return tot;
            }
        }

        public virtual ICollection<Product> Products { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Branch Branch { get; set; }
    }

    public class OrderYearsViewModel
    {
        [DisplayName("שנה")]
        public int year { get; set; }

        [DisplayName("מספר ההזמנות בשנה")]
        public int postCount { get; set; }

    }
}