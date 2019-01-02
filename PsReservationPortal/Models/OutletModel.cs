using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("Outlet")]
    public class OutletModel
    {
        [Key]
        public long OutletId { get; set; }

        [Required]
        [Display(Name ="Outlet Name")]
        public string OutletName { get; set; }

        [Display(Name ="Location Description")]
        public string OutletLocation { get; set; }

        [Display(Name ="Address")]
        public string OutletAddress { get; set; }

        public bool Active { get; set; }

        public CompanyModel Company { get; set; }

    }
}