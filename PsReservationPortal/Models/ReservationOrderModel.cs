using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("ReservationOrder")]
    public class ReservationOrderModel
    {
        public enum ReservationStatus { New, Comfirmed, Expired, Cancelled, Fulfilled }

        [Key]
        public long ReservationNum { get; set; }
        
        public string Name { get; set; }

        
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Phone number has to be at least 7 character long.", MinimumLength = 7)]
        public string PhoneNum { get; set; }
        
        public DateTime ReserveDateTime { get; set; }

        public TableModel TableId { get; set; }

        public String ReserveRequest { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}