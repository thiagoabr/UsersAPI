using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAPI.Infra.Messages.Models
{
    public class AuthRequestModel
    {
        public string Key { get; set; }
        public string Pass { get; set; }

    }
}
