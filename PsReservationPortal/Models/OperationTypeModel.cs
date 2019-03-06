using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("OperationType")]
    public class OperationTypeModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Allow Reserve?")]
        public bool isAllowReserve { get; set; }

        [ForeignKey("Outlet")]
        public long OutletId { get; set; }
        public virtual OutletModel Outlet { get; set; }

        public DateTime DateTimeCreated { get; set; }
        
        public DateTime DateTimeUpdated { get; set; }
    }
}