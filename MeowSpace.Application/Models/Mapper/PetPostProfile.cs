using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MeowSpace.Application.Features.PetPosts.Commands.UpdatePetPost;
using MeowSpace.Application.Models.DTOs;
using MeowSpace.Domain.Entities;

namespace MeowSpace.Application.Models.Mapper
{
    public class PetPostProfile : Profile
    {
        public PetPostProfile()
        {
            CreateMap<PetPost, PetPostDTO>() // Mapping from Entity to DTO
                .ForMember(dest => dest.PostCreationDate, opt => opt.MapFrom(src => src.PostCreationDate));

            CreateMap<PetPostDTO, PetPost>() // Mapping from DTO to Entity (for Create/Update operations)
                .ForMember(dest => dest.PostCreationDate, opt => opt.MapFrom(src => src.PostCreationDate)); // Set date on creation
            
            CreateMap<UpdatePetPostDTO, PetPost>() // Mapping from DTO to Entity (for Create/Update operations)
                .ForMember(dest => dest.PostUpdationDate, opt => opt.MapFrom(src => src.postUpdationDate));
        }
    }
}
