using AutoMapper;
using MediatR;
using MeowSpace.Application.Features.PetPosts.Commands.CreatePetPost;
using MeowSpace.Application.Features.PetPosts.Commands.DeletePetPost;
using MeowSpace.Application.Features.PetPosts.Commands.UpdatePetPost;
using MeowSpace.Application.Features.PetPosts.Queries.GetAllPetPosts;
using MeowSpace.Application.Models.DTOs;
using MeowSpace.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeowSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IMediator _mediator;
        readonly ILogger<PetController> _logger;
        public PetController(IMapper mapper, IMediator mediator, ILogger<PetController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPetPosts()
        {
            var petPosts = await _mediator.Send(new GetAllPetPostsQuery());
            var petPostDtos = _mapper.Map<IEnumerable<PetPostDTO>>(petPosts); // Mapping entities to DTOs
            return Ok(petPostDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePetPost(PetPostDTO petPost)
        {
            if (petPost == null)
            {
                return BadRequest();
            }

            var newPetPost = _mapper.Map<PetPost>(petPost); // Mapping DTO to Entity
            var createdPetPost = await _mediator.Send(new CreatePetPostCommand(newPetPost));
            return CreatedAtAction(nameof(GetAllPetPosts), new { id = newPetPost.PostID }, newPetPost);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePetPost([FromQuery] int postID, UpdatePetPostDTO petPost)
        {
            if (petPost == null)
            {
                return BadRequest();
            }
            var mappedPetPost = _mapper.Map<PetPost>(petPost);
            var updatePetPost = await _mediator.Send(new UpdatePetPostCommand(postID, mappedPetPost));
            return Ok(updatePetPost);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePetPost(int postID)
        {
            var deletePetPost = await _mediator.Send(new DeletePetPostCommand(postID));
            if (deletePetPost == false)
                return NotFound(deletePetPost);
            return Ok(deletePetPost);
        }
    }
}
