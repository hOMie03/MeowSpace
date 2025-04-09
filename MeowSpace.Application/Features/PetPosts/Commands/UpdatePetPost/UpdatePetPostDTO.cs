using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowSpace.Application.Features.PetPosts.Commands.UpdatePetPost
{
    public class UpdatePetPostDTO
    {
        public string postTitle { get; set; }
        public string postDescription { get; set; }
        public DateOnly postUpdationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
