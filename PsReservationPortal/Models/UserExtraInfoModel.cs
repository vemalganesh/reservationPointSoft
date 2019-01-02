using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("UserExtraInfo")]
    public class UserExtraInfoModel
    {
        [Required]
        [Key]
        public string UserId { get; set; }


        public bool Activated { get; set; }


        public bool Suspended { get; set; }

        public virtual ICollection<CompanyModel> Companies { get;set; }
    }
}