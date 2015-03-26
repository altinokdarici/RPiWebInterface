using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPi.Portal.DataAccess
{
    public class User
    {
        public Guid Id { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }

        public virtual List<Device> Devices { get; set; }
    }

}
