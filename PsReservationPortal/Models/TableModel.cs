using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("Table")]
    public class TableModel
    {
        [Key]
        public long Id { get; set; }
        
        public string TableNumber { get; set; }
        
        public int Pax { get; set; }
        
        public bool isActive { get; set; }

        [ForeignKey("Outlet")]
        public long OutletId { get; set; }

        public virtual OutletModel Outlet { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}