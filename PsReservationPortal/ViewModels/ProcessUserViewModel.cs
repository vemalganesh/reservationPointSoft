using PsReservationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsReservationPortal.ViewModels
{
    public class ProcessUserViewModel
    {
        public UserRegistrationInfoModel UserRegistrationInfo { get; set; }

        public List<CompanyModel> Companies { get; set; }

    }
}