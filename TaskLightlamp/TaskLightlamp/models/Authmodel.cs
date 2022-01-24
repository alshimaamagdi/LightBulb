using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskLightlamp.models
{
    public class Authmodel
    {
        public string message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List <string>Roles { get; set; }
        public string Token { get; set; }

        public DateTime Expireon { get; set; }


    }
}
