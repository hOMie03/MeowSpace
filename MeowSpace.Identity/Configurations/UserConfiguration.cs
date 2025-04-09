using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MeowSpace.Identity.Models;

namespace MeowSpace.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "41776062 - 1111 - 1aba - a111 - 2879a6680b9a",
                    UserName = "MeowSpaceAdmin",
                    NormalizedUserName = "MEOWSPACEADMIN",
                    Email = "admin@meowspace.lol",
                    NormalizedEmail = "ADMIN@MEOWSPACE.LOL",
                    PetName = "Meowdmin",
                    PetType = Domain.Constants.PetType.Admin,
                    PasswordHash = hasher.HashPassword(null, "Meowoof!1!"),
                    PetProfileImg = "https://media.makeameme.org/created/the-admin-has-5c9be2.jpg"
                },
                new ApplicationUser
                {
                    Id = "41776062 - 2222 - 1bbb - a222 - 2879a6680b9a",
                    UserName = "dharampal_official",
                    NormalizedUserName = "DHARAMPAL_OFFICIAL",
                    Email = "dharampal@woofwoof.com",
                    NormalizedEmail = "DHARAMPAL@WOOFWOOF.COM",
                    PetName = "Dharampal",
                    PetType = Domain.Constants.PetType.Dawg,
                    PasswordHash = hasher.HashPassword(null, "PalPaw@69"),
                    PetProfileImg = "https://i.pinimg.com/736x/bc/08/e9/bc08e964513eb63238efabde6cd193d4.jpg"
                }
            );
        }
    }
}
