using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace RPi.Portal.DataAccess
{
    public class Entity : IDisposable
    {
        private readonly string JsonUrl = HttpContext.Current.Server.MapPath("~/App_Data/Users.json");

        [MethodImpl(MethodImplOptions.Synchronized)]
        public User GetUser(string email, string password)
        {
            List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(JsonUrl));
            return users.FirstOrDefault(x => x.EMail == email && x.Password == password);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public User GetUser(Guid Id)
        {
            List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(JsonUrl));
            return users.FirstOrDefault(x => x.Id == Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SaveUser(User user)
        {
            List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(JsonUrl));
            users.Add(user);
            System.IO.File.WriteAllText(JsonUrl, Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateUser(User user)
        {
            List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(JsonUrl));
            var u = users.FirstOrDefault(x => x.Id == user.Id);
            if (u != null)
            {
                u.Password = user.Password;
                u.EMail = user.EMail;
            }
            else
            {
                SaveUser(user);
            }
            System.IO.File.WriteAllText(JsonUrl, Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDevice(string code, Guid userId)
        {
            List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(JsonUrl));
            var u = users.FirstOrDefault(x => x.Id == userId);
            if (u != null)
            {
                Device d = u.Devices.FirstOrDefault(x => x.Code == code);
                if (d == null)
                {
                    d = new Device();
                    d.Id = Guid.NewGuid();
                    d.Code = code;
                    u.Devices.Add(d);
                }
            }
            System.IO.File.WriteAllText(JsonUrl, Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }


        public void Dispose()
        {

        }
    }

}
