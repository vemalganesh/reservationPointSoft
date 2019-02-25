using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("CloseDateSetting")]
    public class CloseDateSettingModel
    { 
        [Key]
        public int Id { get; set; }

        public string Remark { get; set; }
        
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }
        
        public DateTime DateTimeUpdated { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public OutletModel OutletId { get; set; }
    }
}