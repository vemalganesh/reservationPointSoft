using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("ReservationHourSetting")]
    public class ReservationHourSettingModel
    {
        [Key]
        public long Id { get; set; }
        
        public double MonStartTime { get; set; }
        
        public double MonEndTime { get; set; }

        public double TueStartTime { get; set; }

        public double TueEndTime { get; set; }

        public double WedStartTime { get; set; }

        public double WedEndTime { get; set; }

        public double ThurStartTime { get; set; }

        public double ThurEndTime { get; set; }

        public double FriStartTime { get; set; }

        public double FriEndTime { get; set; }

        public double SatStartTime { get; set; }

        public double SatEndTime { get; set; }

        public double SunStartTime { get; set; }

        public double SunEndTime { get; set; }

        [Required]
        public OutletModel OutletId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}