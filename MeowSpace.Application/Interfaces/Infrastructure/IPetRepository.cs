using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Domain.Entities;

namespace MeowSpace.Application.Interfaces.Infrastructure
{
    public interface IPetRepository
    {
        Task<IEnumerable<PetPost>> GetAllPetPostsAsync();
        Task<PetPost> CreatePetPostAsync(PetPost petPost);
        Task<PetPost> UpdatePetPostAsync(int postID, PetPost petPost);
        Task<bool> DeletePetPostAsync(int postID);
    }
}
