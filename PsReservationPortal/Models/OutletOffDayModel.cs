using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("OutletOffDay")]
    public class OutletOffDayModel
    { 
        [Key]
        public int Id { get; set; }

        public int OutletId { get; set; }

        [Display(Name = "Monday")]
        public bool MonOff { get; set; }

        [Display(Name = "Tuesday")]
        public bool TueOff { get; set; }

        [Display(Name = "Wednesday")]
        public bool WedOff { get; set; }

        [Display(Name = "Thursday")]
        public bool ThuOff { get; set; }

        [Display(Name = "Friday")]
        public bool FriOff { get; set; }

        [Display(Name = "Saturday")]
        public bool SatOff { get; set; }

        [Display(Name = "Sunday")]
        public bool SunOff { get; set; }

        public bool Active { get; set; }

        public CompanyModel Company { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public int ModifiedUserId { get; set; }
    }
}