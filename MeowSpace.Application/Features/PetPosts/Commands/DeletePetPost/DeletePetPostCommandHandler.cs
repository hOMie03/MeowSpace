using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeowSpace.Application.Interfaces.Infrastructure;

namespace MeowSpace.Application.Features.PetPosts.Commands.DeletePetPost
{
    public class DeletePetPostCommandHandler : IRequestHandler<DeletePetPostCommand, bool>
    {
        readonly IPetRepository _petRepository;
        public DeletePetPostCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;

        }
        public Task<bool> Handle(DeletePetPostCommand request, CancellationToken cancellationToken)
        {
            var deletingPetPost = _petRepository.DeletePetPostAsync(request.postID);
            return deletingPetPost;
        }
    }
}
