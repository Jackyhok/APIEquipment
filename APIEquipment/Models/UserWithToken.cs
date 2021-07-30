using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEquipment.Models
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.Username = user.Username;            
            this.Password = user.Password;
            this.Roles = user.Roles;
        }
    }
}
