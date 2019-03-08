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
        public long Id { get; set; }

        [Required]
        [Display(Name ="Outlet Name")]
        public string Name { get; set; }

        [Display(Name ="Location")]
        public string Location { get; set; }

        [Display(Name ="Address")]
        public string Address { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public bool isActive { get; set; }
        
        public double ReservationDuration { get; set; }
        
        public double ReservationAllowBefore { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Phone number has to be at least 7 character long.", MinimumLength = 7)]
        public string PhoneNum { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Phone number has to be at least 7 character long.", MinimumLength = 7)]
        public string ContactPersonPhoneNum { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }

        [ForeignKey("Company")]
        public long CompanyId { get; set; }

        public virtual CompanyModel Company { get; set; }

        public virtual ICollection<ReservationExclusionDateModel> ReservationExclusionDates { get; set; }

        public virtual ICollection<OperationHourSettingModel> OperationHourSettings { get; set; }
        
        public virtual ReservationExclusionDayModel ReservationExclusionDay { get; set; }

        public virtual ICollection<UserExtraInfoModel> Managers { get; set; }

        public virtual ICollection<TableModel> Tables { get; set; }

        public virtual ICollection<OperationTypeModel> OperationTypes { get; set; }
    }
}