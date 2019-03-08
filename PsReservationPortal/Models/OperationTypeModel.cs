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
        public bool IsAllowReserve { get; set; }

        [ForeignKey("Outlet")]
        public long OutletId { get; set; }
        public virtual OutletModel Outlet { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}