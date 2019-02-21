using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("ReservationSetting")]
    public class ReservationSettingModel
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public double ReservationDuration { get; set; }
        
        public double ReservationAllowBefore { get; set; }
        
        public CompanyModel CompanyId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}