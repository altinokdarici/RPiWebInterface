using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RPi.Portal.Helpers
{
    class SessionHelper
    {
        public static DataAccess.User User
        {
            get
            {
                return HttpContext.Current.Session["User"] as DataAccess.User;
            }
            set
            {
                HttpContext.Current.Session["User"] = value;
            }
        }
    }
}
