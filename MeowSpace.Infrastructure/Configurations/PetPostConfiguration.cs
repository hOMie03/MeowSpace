using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MeowSpace.Infrastructure.Configurations
{
    public class PetPostConfiguration : IEntityTypeConfiguration<PetPost>
    {
        public void Configure(EntityTypeBuilder<PetPost> builder)
        {
            builder.HasData(
                new PetPost()
                {
                    PostID = 1,
                    PostTitle = "Dharampal",
                    PostDescription = "Dharampal",
                    PostImage = "https://i.pinimg.com/736x/bc/08/e9/bc08e964513eb63238efabde6cd193d4.jpg",
                    UserID = "41776062 - 2222 - 1bbb - a222 - 2879a6680b9a"
                }
            );
        }
    }
}
