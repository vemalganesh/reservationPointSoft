using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PsReservationPortal.Models;

namespace PsReservationPortal.ViewModels
{
    public class AssignOutletViewModel
    {
        public List<OutletModel> Outlets { get; set; }
        public UserInfoViewModel User { get; set; }
    }
}