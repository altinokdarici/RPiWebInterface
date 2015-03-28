using System.Web;

namespace RPi.Portal.Helpers
{
    public class SessionHelper
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
