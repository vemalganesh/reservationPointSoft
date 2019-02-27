using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("CustTable")]
    public class CustTableModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        [MaxLength(60)]
        public string Name { get; set; }

        [Display(Name="Registration Number")]
        public string RegNumber { get; set; }
        
        public bool isActive { get; set; }

        public virtual ICollection<UserExtraInfoModel> UserExtraInfos { get; set; }
        public virtual ICollection<OutletModel> Outlets { get; set; }
    }
}