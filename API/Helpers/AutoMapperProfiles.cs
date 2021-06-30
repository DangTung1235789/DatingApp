using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    //Adding AutoMapper to solve vòng lặp (AppUser <=> Photos)
    //Mapp is it help us map from 1 obj to another 
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //where we specify in here is where we want to map from and where we want to map to 
            //we know we want to go from our AppUser to our memberDto
            //that takes care of the mapping from our AppUser to our member
            //that we need for our mapping profiles 
            //we need to do because we're going to be adding this as a dependency that we can inject 
            //we need to add this to our application service extentions
            CreateMap<AppUser, MemberDto>()
            //- we can add some configuration ForMember: we want to affect 
            //- the first we pass is the destination => what property are we looking to affect ? => PhotoUrl
            //option looking for MapFrom: is can tell where we want it to map from very specifically
            //- where do we want to map from ?
            //we want to map from our "source"
            //- when we map an individual property, we give it the destination property, the photo URL, we tell it where
            //we want to map from and the source of where we're mapping from
            //- and we're going to our users photo collection, and get first photo or default that is main and get
            //the URL from that 
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            //this is our new mapping configuration 
            CreateMap<Photo, PhotoDto>();
            //because it's a DTOs and we're going to want to map this into our user entity 
            CreateMap<MemberUpdateDto,AppUser>();
            //10. Updating the API register method
            CreateMap<RegisterDTO, AppUser>();
        }
    }
}