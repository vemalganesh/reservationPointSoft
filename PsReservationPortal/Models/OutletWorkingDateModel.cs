using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("OutletWorkingDate")]
    public class OutletWorkingDateModel
    { 
        [Key]
        public int Id { get; set; }

        public int OutletId { get; set; }

        [Display(Name = "Date From")]
        public DateTime BusinessDateFrom { get; set; }

        [Display(Name = "Date To")]
        public DateTime BusinessDateTo { get; set; }

        public bool WorkDateStatus { get; set; }

        public CompanyModel Company { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public int ModifiedUserId { get; set; }

        public DateTime CreateddDateTime { get; set; }
    }
}