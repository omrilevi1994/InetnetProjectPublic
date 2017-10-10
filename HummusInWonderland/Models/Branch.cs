using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HummusInWonderland.Models
{
    public class Branch
    {
        public int BranchID { get; set; }

        [Display(Name = "שם החנות")]
        [Required(ErrorMessage = "שדה חובה")]
        public string BranchName { get; set; }

        [Display(Name = "עיר")]
        [Required(ErrorMessage = "שדה חובה")]
        public string BranchCity { get; set; }

        [Display(Name = "רחוב")]
        [Required(ErrorMessage = "שדה חובה")]
        public string BranchStreet { get; set; }

        public string DisplayName
        {
            get
            {
                return this.BranchName + " (" + this.BranchCity + " - " + this.BranchStreet + ")";
            }
        }

        [Display(Name = "מספר בית")]
        [Required(ErrorMessage = "שדה חובה")]
        public int BranchsHouseNumber { get; set; }

        [Display(Name = "מספר טלפון")]
        public string BranchsPhoneNumber { get; set; }

        [Display(Name = "נקודת רוחב")]
        [Required(ErrorMessage = "שדה חובה")]
        public double CoordX { get; set; }

        [Display(Name = "נקודת אורך")]
        [Required(ErrorMessage = "שדה חובה")]
        public double CoordY { get; set; }
    }
}