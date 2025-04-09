using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeowSpace.Domain.Entities;

namespace MeowSpace.Application.Features.PetPosts.Commands.UpdatePetPost
{
    public record UpdatePetPostCommand(int postID, PetPost updatedPetPost) : IRequest<PetPost>;
}
