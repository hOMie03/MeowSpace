using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowSpace.Application.Models.Identity
{
    public class RefreshTokenResponse
    {
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string JwtToken { get; set; }
    }
}
