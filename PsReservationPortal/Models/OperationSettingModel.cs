using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("OperationSetting")]
    public class OperationSettingModel
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }

        public enumDays enumDays { get; set; }
        
        public double OpenHours { get; set; }
        
        public double ClosingHours { get; set; }
        
        public CompanyModel CompanyId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }

    public enum enumDays
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
}