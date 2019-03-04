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

        public Days Day { get; set; }
        
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public int StartMinute { get; set; }

        public int EndMinute { get; set; }

        [Required]
        public OutletModel OutletId { get; set; }

        [Required]
        public OperationTypeModel OperationTypeId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}