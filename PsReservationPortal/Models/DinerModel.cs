using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("Diner")]
    public class DinerModel
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Phone number has to be at least 7 character long.", MinimumLength = 7)]
        public string PhoneNum { get; set; }
        
        public DateTime ReserveTime { get; set; }

        public virtual TableModel TableId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}