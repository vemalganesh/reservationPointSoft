using Microsoft.AspNet.Identity.EntityFramework;
using PsReservationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsReservationPortal.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Activated { get; set; }
        public bool Suspended { get; set; }
        public MultiSelectList Companies { get; set; }
        public MultiSelectList UserRoles { get; set; }
        //public IEnumerable<CompanyModel> Companies { get; set; }
        //public IEnumerable<IdentityRole> UserRoles { get; set; }
    }
}