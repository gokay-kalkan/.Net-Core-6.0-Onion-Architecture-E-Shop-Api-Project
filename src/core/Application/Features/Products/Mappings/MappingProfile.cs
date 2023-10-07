using Application.Dtos.CategoryDtos;
using Application.Dtos.ProductDtos;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductCreateDto>().ReverseMap();

            CreateMap<ProductCreateDto, CreateProductCommand>().ReverseMap();

            CreateMap<Product, CreateProductCommand>().ReverseMap();

            CreateMap<Product, ProductDeleteDto>().ReverseMap();

            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<Product, ProductListDto>().ReverseMap();

            CreateMap<Product, ListProductQuery>().ReverseMap();

            CreateMap<ProductListDto, ListProductQuery>().ReverseMap();



            CreateMap<Product, GetByIdProductQuery>().ReverseMap();

            CreateMap<Product, ProductGetByIdDto>().ReverseMap();

           

            CreateMap<DeleteProductCommand, ProductDeleteDto>().ReverseMap();

            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<UpdateProductCommand, ProductUpdateDto>().ReverseMap();

            CreateMap<UpdateProductCommand, Product>().ReverseMap();


            CreateMap<ProductListByCategoryDto, Product>().ReverseMap();

            CreateMap<ProductListByCategoryQuery, Product>().ReverseMap();

            CreateMap<ProductListByCategoryQuery, ProductListByCategoryDto>().ReverseMap();

        }
    }
}
