using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Domain.Constants;

namespace MeowSpace.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required, NotNull]
        public string Username { get; set; }

        [Required, NotNull]
        public string PetName { get; set; }

        [Required, EmailAddress, NotNull]
        public string Email { get; set; }

        [Required, NotNull, RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&-+=()])(?=\\S+$).{4,10}$")]
        public string Password { get; set; }

        [Required, NotNull]
        public PetType PetType { get; set; }

        [Required, NotNull]
        public string PetProfileImg { get; set; }
    }
}
