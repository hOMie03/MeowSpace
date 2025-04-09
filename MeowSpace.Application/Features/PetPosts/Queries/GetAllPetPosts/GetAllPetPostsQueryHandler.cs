using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Transactions;
using MeowSpace.Application.Interfaces.Infrastructure;
using MeowSpace.Domain.Entities;

namespace MeowSpace.Application.Features.PetPosts.Queries.GetAllPetPosts
{
    public class GetAllPetPostsQueryHandler : IRequestHandler<GetAllPetPostsQuery, IEnumerable<PetPost>>
    {
        readonly IPetRepository _petRepository;
        // Constructor
        public GetAllPetPostsQueryHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }
        public async Task<IEnumerable<PetPost>> Handle(GetAllPetPostsQuery request, CancellationToken cancellationToken)
        {
            var allPetPosts = await _petRepository.GetAllPetPostsAsync();
            return allPetPosts;
        }
    }
}
