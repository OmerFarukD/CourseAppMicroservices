using AutoMapper;
using CourseApp.Service.Catalog.Dtos;
using CourseApp.Service.Catalog.Models;

namespace CourseApp.Service.Catalog.Profiles;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Course, CourseDto>()
            .ForMember(d=>d.CategoryDto,opt=>opt.MapFrom(s=>s.Category))
            .ReverseMap();
        
        CreateMap<Course, CourseCreateDto>().ReverseMap();
        
        CreateMap<Course, CourseUpdateDto>().ReverseMap();
        
        CreateMap<Feature, FeatureDto>().ReverseMap();
    }
}