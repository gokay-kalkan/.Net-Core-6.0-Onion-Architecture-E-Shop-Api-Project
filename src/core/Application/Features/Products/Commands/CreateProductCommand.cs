

using Application.Dtos.ProductDtos;
using Application.Exceptions;
using Application.FluentValidations.ProductFluentValidations;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.SmartUrl;
using System.Net;

namespace Application.Features.Products.Commands
{
    public class CreateProductCommand:IRequest<ProductCreateDto>
    {
        public IFormFile? image{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }
        public string? ImageUrl { get; set; }

        
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CreateProductCommand()
        {
            
        }
    }

    public class CreateProductCommandHandle : IRequestHandler<CreateProductCommand, ProductCreateDto>
    {
        private readonly IProductRepository repository;

        private readonly IMapper mapper;

        public CreateProductCommandHandle(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ProductCreateDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            //fluent valdiation
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .Select(group => $"{group.Key}: {string.Join(", ", group.Select(error => error.ErrorMessage))}")
                    .ToList();

                // Doğrulama hatasını yeni AppException ile fırlatın
                throw new AppException((int)HttpStatusCode.BadRequest, "Doğrulama hatası", validationErrors);
            }

            Product product = mapper.Map<Product>(request);


            product.ImageUrl = request.image.FileName;
            product.Status = true;
            product.SmartUrl = UrlHelper.GenerateSlug(product.Name);
            Product createProduct = await repository.AddAsync(product);

            ProductCreateDto createProductDto = mapper.Map<ProductCreateDto>(createProduct);
            return createProductDto;
        }
    }
}
