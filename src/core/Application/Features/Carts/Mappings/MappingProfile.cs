
using Application.Dtos.CartDtos;
using Application.Dtos.CategoryDtos;
using Application.Features.Carts.Commands;
using Application.Features.Carts.Queries;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Carts.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Cart, CartCreateDto>().ReverseMap();

            CreateMap<CartCreateDto, CreateCartCommand>().ReverseMap();

            CreateMap<Cart, CreateCartCommand>().ReverseMap();



            CreateMap<Cart, CartListDto>().ReverseMap();

            CreateMap<Cart, CartGetByIdDto>().ReverseMap();

            CreateMap<Cart, GetByIdCartQuery>().ReverseMap();


            CreateMap<Cart, CartDeleteDto>().ReverseMap();

            CreateMap<DeleteCartCommand, CartDeleteDto>().ReverseMap();

            CreateMap<UpdateCartCommand, CartUpdateDto>().ReverseMap();

            CreateMap<Cart, CartUpdateDto>().ReverseMap();

            CreateMap<UpdateCartCommand, Cart>().ReverseMap();



            CreateMap<Cart, CartGetByUserIdListDto>().ReverseMap();

            CreateMap<Cart, GetByUserIdListCartQuery>().ReverseMap();
        }
    }
}
