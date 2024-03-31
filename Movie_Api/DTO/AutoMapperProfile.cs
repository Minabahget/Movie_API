using AutoMapper;
using Movie_Api.Models;
namespace Movie_Api.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryWithMovie>()
             .ForMember(dest => dest.MovieNames, opt => opt.MapFrom(src => src.Movies.Select(m => m.Title)));
            CreateMap<Movie, MovieWithCategory>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(m => m.Name)));
        }
    }
}
