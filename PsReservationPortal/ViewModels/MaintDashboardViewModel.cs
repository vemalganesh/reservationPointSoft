using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PsReservationPortal.Models;

namespace PsReservationPortal.ViewModels
{
    public class MaintDashboardViewModel
    {
        public List<UserRegistrationInfoModel> UserRegistrationInfoList { get; set; }

        public List<UserInfoViewModel> UserInfoList { get; set; }
    }
}