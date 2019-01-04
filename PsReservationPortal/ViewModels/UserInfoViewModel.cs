using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsReservationPortal.ViewModels
{
    public class UserInfoViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Activated { get; set; }
        public bool Suspended { get; set; }
        public List<string> Companies { get; set; }
        public List<string> UserRoles { get; set; }


    }
}