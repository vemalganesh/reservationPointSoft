using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("ReservationExclusionDate")]
    public class ReservationExclusionDateModel
    {
        [Key]
        public int Id { get; set; }

        public string ExlusionDateName { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Date To")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTo { get; set; }

        public DateTime DateTimeUpdated { get; set; }

        public DateTime DateTimeCreated { get; set; }

        [ForeignKey("Outlet")]
        public long OutletId { get; set; }
        public virtual OutletModel Outlet { get; set; }
    }
}