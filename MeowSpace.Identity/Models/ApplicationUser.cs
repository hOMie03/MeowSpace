using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace MeowSpace.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, NotNull]
        public string PetName { get; set; }

        [Required, NotNull]
        public string PetProfileImg { get; set; }

        [Required, NotNull]
        public PetType PetType { get; set; }
    }
}
