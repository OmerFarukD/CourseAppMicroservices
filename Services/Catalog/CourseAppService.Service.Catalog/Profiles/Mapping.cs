using AutoMapper;
using CourseAppService.Service.Catalog.Dtos;
using CourseAppService.Service.Catalog.Models;

namespace CourseAppService.Service.Catalog.Profiles;

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