using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PsReservationPortal.Models
{
    [Table("ReservationExclusionDay")]
    public class ReservationExclusionDayModel
    {
        public enum OpenClose { Open, Close }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Monday")]
        public OpenClose Monday { get; set; }

        [Display(Name = "Tuesday")]
        public OpenClose Tuesday { get; set; }

        [Display(Name = "Wednesday")]
        public OpenClose Wednesday { get; set; }

        [Display(Name = "Thursday")]
        public OpenClose Thursday { get; set; }

        [Display(Name = "Friday")]
        public OpenClose Friday { get; set; }

        [Display(Name = "Saturday")]
        public OpenClose Saturday { get; set; }

        [Display(Name = "Sunday")]
        public OpenClose Sunday { get; set; }

        public DateTime DateTimeUpdated { get; set; }

        public DateTime DateTimeCreated { get; set; }

        [Required]
        public OutletModel OutletId { get; set; }

    }
}