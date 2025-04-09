using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeowSpace.Domain.Entities;

namespace MeowSpace.Application.Features.PetPosts.Queries.GetAllPetPosts
{
    public record GetAllPetPostsQuery() : IRequest<IEnumerable<PetPost>>;
}
