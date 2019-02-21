using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("Outlet")]
    public class OutletModel
    {
        [Key]
        public long OutletId { get; set; }

        [Required]
        [Display(Name ="Outlet Name")]
        public string OutletName { get; set; }

        [Display(Name ="Location Description")]
        public string OutletLocation { get; set; }

        [Display(Name ="Address")]
        public string OutletAddress { get; set; }

        public bool isActive { get; set; }

        public CompanyModel Company { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Phone number has to be at least 7 character long.", MinimumLength = 7)]
        public string PhoneNum { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Phone number has to be at least 7 character long.", MinimumLength = 7)]
        public string ContactPersonPhoneNum { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }

        public ICollection<UserExtraInfoModel> Managers { get; set; }

        public ReservationSettingModel ReservationSettingId { get; set; }

        public OperationSettingModel OperationSettingId { get; set; }

        public ICollection<TableModel> Tables { get; set; }
    }
}