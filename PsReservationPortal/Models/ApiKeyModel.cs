using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsReservationPortal.Models
{
    [Table("AspNetApiKey")]
    public class ApiKeyModel
    {
        [Key]
        public string ApiKey { get; set; }
        public int Company_Id { get; set; }


    }
}