using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeowSpace.Application.Interfaces.Infrastructure;
using MeowSpace.Domain.Entities;

namespace MeowSpace.Application.Features.PetPosts.Commands.CreatePetPost
{
    public class CreatePetPostCommandHandler : IRequestHandler<CreatePetPostCommand, PetPost>
    {
        readonly IPetRepository _petRepository;
        public CreatePetPostCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;

        }
        public Task<PetPost> Handle(CreatePetPostCommand request, CancellationToken cancellationToken)
        {
            var newPost = _petRepository.CreatePetPostAsync(request.petPost);
            return newPost;
        }
    }
}
