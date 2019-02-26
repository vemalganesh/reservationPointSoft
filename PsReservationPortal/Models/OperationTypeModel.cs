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

        public string Desc {get;set;}

        public bool isAllowReserve { get; set; }
    }
}