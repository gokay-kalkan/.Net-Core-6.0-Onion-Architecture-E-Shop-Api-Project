using Application.Dtos.CategoryDtos;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using AutoMapper;
using Domain.Entities;
using System.Net.Http.Headers;

namespace Application.Features.Categories.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryCreateDto>().ReverseMap();

            CreateMap<CategoryCreateDto, CreateCategoryCommand>().ReverseMap();

            CreateMap<Category, CreateCategoryCommand>().ReverseMap();

 
            CreateMap<Category, CategoryListDto>().ReverseMap();

            CreateMap<Category, CategoryGetByIdDto>().ReverseMap();

            CreateMap<Category, GetByIdCategoryQuery>().ReverseMap();

            CreateMap<Category, CategoryDeleteDto>().ReverseMap();

            CreateMap<DeleteCategoryCommand, CategoryDeleteDto>().ReverseMap();

            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<UpdateCategoryCommand, CategoryUpdateDto>().ReverseMap();

            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();


        }
    }
}
