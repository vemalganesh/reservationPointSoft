using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("OperationHourSetting")]
    public class OperationHourSettingModel
    {
        [Key]
        public int Id { get; set; }

        public Days day { get; set; }

        public double OpenHour { get; set; }

        public double CloseHour { get; set; }

        public double StartBreak { get; set; }

        public double EndBreak { get; set; }

        public double StartResvPeriod { get; set; }

        public double EndResvPeriod { get; set; }

        [Required]
        public OutletModel OutletId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }

    public enum Days
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