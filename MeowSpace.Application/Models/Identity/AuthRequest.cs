using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowSpace.Application.Models.Identity
{
    public class AuthRequest
    {
        public string EmailorUsername { get; set; }
        public string Password { get; set; }
    }
}
