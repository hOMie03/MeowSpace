using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MeowSpace.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "41116008 - 6086 - 1fbf - b923 - 2879a6680b9a", // Admin
                    UserId = "41776062 - 1111 - 1aba - a111 - 2879a6680b9a" // Meowdmin
                },
                new IdentityUserRole<string>
                {
                    RoleId = "41226008 - 6086 - 1fbf - b923 - 2879a6680b9a", // User
                    UserId = "41776062 - 2222 - 1bbb - a222 - 2879a6680b9a" // Dharampal
                }
            );
        }
    }
}
