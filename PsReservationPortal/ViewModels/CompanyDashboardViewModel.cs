﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PsReservationPortal.Models;

namespace PsReservationPortal.ViewModels
{
    public class CompanyDashboardViewModel
    {
        public List<OutletModel> Outlets { get; set; }
        public List<UserInfoViewModel> Users { get; set; }
        public CompanyModel Company { get; set; }
    }
}