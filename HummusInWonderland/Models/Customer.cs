using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HummusInWonderland.Models
{
    public enum Gender
    {
        male,
        female
    };

    public class Customer
    {
        public int CustomerID { get; set; }

        [Display(Name = "שם פרטי")]
        [Required(ErrorMessage = "שדה חובה")]
        public string FirstName { get; set; }

        [Display(Name = "שם משפחה")]
        [Required(ErrorMessage = "שדה חובה")]
        public string LastName { get; set; }

        public string DisplayName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        [Display(Name = "תאריך לידה")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "שדה חובה")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "מין")]
        public Gender Gender { get; set; }

        [Display(Name = "אימייל")]
        [Required(ErrorMessage = "שדה חובה")]
        [EmailAddress(ErrorMessage = "הכתובת אינה כתובה אימייל תקינה")]
        public string Email { get; set; }

        [Display(Name = "סיסמה")]
        [Required(ErrorMessage = "שדה חובה")]
        [StringLength(100, ErrorMessage = "הסיסמה צריכה להכיל מינימום 6 תווים", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "עיר")]
        public string City { get; set; }

        [Display(Name = "רחוב")]
        public string Street { get; set; }

        [Display(Name = "מספר טלפון")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
