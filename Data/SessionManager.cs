using KSVA2._0_WPF.Models;
using KSVA2._0_WPF.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KSVA2._0_WPF.Data
{
    
    public static class SessionManager
    {
        public static user? CurrentUser { get; private set; }

        public static void InitializeSession(user user)
        {
            CurrentUser = user;
        }

        public static void ClearSession()
        {
            CurrentUser = null;
        }
    }
}
