using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Application.Exceptions;
using MeowSpace.Application.Interfaces.Infrastructure;
using MeowSpace.Domain.Entities;
using MeowSpace.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MeowSpace.Infrastructure.Repositories
{
    public class PetRepository : IPetRepository
    {
        readonly MeowSpaceDBContext _msdbContext;
        readonly ILogger<PetRepository> _logger;
        public PetRepository(MeowSpaceDBContext msdbContext, ILogger<PetRepository> logger)
        {
            _msdbContext = msdbContext;
            _logger = logger;
        }
        public async Task<IEnumerable<PetPost>> GetAllPetPostsAsync()
        {
            _logger.LogInformation("GetAllPetPostsAsync() Method Initiated");
            var allPetPosts = await _msdbContext.PetPosts.ToListAsync();
            _logger.LogInformation("GetAllPetPostsAsync() Method Performed");
            return allPetPosts;
        }

        public async Task<PetPost> CreatePetPostAsync(PetPost petPost)
        {
            _logger.LogInformation("CreatePetPostAsync() Method Initiated");
            var newPetPost = await _msdbContext.PetPosts.AddAsync(petPost);
            await _msdbContext.SaveChangesAsync();
            _logger.LogInformation("CreatePetPostAsync() Method Performed");
            return petPost;
        }

        public async Task<PetPost> UpdatePetPostAsync(int postID, PetPost petPost)
        {
            _logger.LogInformation("UpdatePetPostAsync() Method Initiated");
            var findPost = await _msdbContext.PetPosts.FirstOrDefaultAsync(p => p.PostID == postID);
            if(findPost == null)
            {
                throw new PostNotFoundException($"Post with ID {postID} was not found!");
            }
            findPost.PostTitle = petPost.PostTitle;
            findPost.PostDescription = petPost.PostDescription;
            findPost.PostUpdationDate = petPost.PostUpdationDate;
            _msdbContext.PetPosts.Update(findPost);
            await _msdbContext.SaveChangesAsync();
            _logger.LogInformation("UpdatePetPostAsync() Method Performed");
            return findPost;
        }
        public async Task<bool> DeletePetPostAsync(int postID)
        {
            _logger.LogInformation("DeletePetPostAsync() Method Initiated");
            var findPost = await _msdbContext.PetPosts.FirstOrDefaultAsync(p => p.PostID == postID);
            if (findPost == null)
            {
                throw new PostNotFoundException($"Post with ID {postID} was not found!");
            }
            _msdbContext.PetPosts.Remove(findPost);
            var checkPostDeletion = await _msdbContext.SaveChangesAsync();
            _logger.LogInformation("DeletePetPostAsync() Method Performed");
            if (checkPostDeletion > 0)
                return true;
            else
                throw new BadRequestException($"For some reasons, post is NOT deleted.");
        }
    }
}
