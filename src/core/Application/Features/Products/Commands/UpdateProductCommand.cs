


using Application.Dtos.CategoryDtos;
using Application.Dtos.ProductDtos;
using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.SmartUrl;

namespace Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ProductUpdateDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

      
        public int Stock { get; set; }
        public IFormFile? newImage { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateProductCommanddHandle : IRequestHandler<UpdateProductCommand, ProductUpdateDto>
    {
        private readonly IProductRepository repository;

        private readonly IMapper mapper;

        public UpdateProductCommanddHandle(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ProductUpdateDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            
            var dto = mapper.Map<Product>(request);

            dto.ImageUrl = request.newImage.FileName;

            dto.SmartUrl = UrlHelper.GenerateSlug(request.Name);

            Product updateProduct = await repository.UpdateAsync(dto);

            ProductUpdateDto updateProductDto = mapper.Map<ProductUpdateDto>(updateProduct);

            return updateProductDto;
        }
    }
}
