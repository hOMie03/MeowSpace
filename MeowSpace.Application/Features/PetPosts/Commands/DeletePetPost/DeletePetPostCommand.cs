using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MeowSpace.Application.Features.PetPosts.Commands.DeletePetPost
{
    public record DeletePetPostCommand(int postID) : IRequest<bool>;
}
