using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeowSpace.Application.Interfaces.Infrastructure;
using MeowSpace.Domain.Entities;

namespace MeowSpace.Application.Features.PetPosts.Commands.UpdatePetPost
{
    public class UpdatePetPostCommandHandler : IRequestHandler<UpdatePetPostCommand, PetPost>
    {
        readonly IPetRepository _petRepository;
        public UpdatePetPostCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;

        }
        public Task<PetPost> Handle(UpdatePetPostCommand request, CancellationToken cancellationToken)
        {
            var updatedPost = _petRepository.UpdatePetPostAsync(request.postID, request.updatedPetPost);
            return updatedPost;
        }
    }
}
