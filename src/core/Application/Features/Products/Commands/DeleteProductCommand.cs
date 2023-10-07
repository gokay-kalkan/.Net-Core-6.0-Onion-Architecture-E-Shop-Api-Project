

using Application.Dtos.CategoryDtos;
using Application.Dtos.ProductDtos;
using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Commands
{
    public class DeleteProductCommand:IRequest<ProductDeleteDto>
    {
        public int Id { get; set; }
    }


    public class DeleteProductCommandHandle : IRequestHandler<DeleteProductCommand, ProductDeleteDto>
    {
        private readonly IProductRepository repository;

        private readonly IMapper mapper;

        public DeleteProductCommandHandle(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<ProductDeleteDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var product = repository.GetById(request.Id);
            if (product == null)
            {
                throw new AppException(404, "Ürün Bulunamadı");
            }
            product.Status = false;
            repository.Update(product);

            var dto = mapper.Map<ProductDeleteDto>(product);

            return Task.FromResult(dto);
        }
    }
}
