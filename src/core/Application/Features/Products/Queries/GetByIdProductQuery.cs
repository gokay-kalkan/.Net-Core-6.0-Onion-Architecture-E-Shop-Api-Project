

using Application.Dtos.CategoryDtos;
using Application.Dtos.ProductDtos;
using Application.Exceptions;
using Application.Features.Categories.Queries;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries
{
    public class GetByIdProductQuery:IRequest<ProductGetByIdDto>
    {
        public int Id { get; set; }
        
    }

    public class GetByIdProductQueryHandle : IRequestHandler<GetByIdProductQuery, ProductGetByIdDto>
    {
        private readonly IProductRepository repository;

        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GetByIdProductQueryHandle(IProductRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }


        public async Task<ProductGetByIdDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            Product product = repository.GetById(request.Id);

            if (product == null)
            {
                throw new AppException(404, "Ürün Bulunamadı");
            }

           var category= await GetCategoryByIdAsync(product.CategoryId);

            ProductGetByIdDto productGetByIdDto = mapper.Map<ProductGetByIdDto>(product);
            productGetByIdDto.CategoryName = category?.Name; // Kategori adını ayarlayın

            return productGetByIdDto;
        }

        //ilgili producta ait kategorileri çekip ilgili product nesnesine ilgili kategori bilgilerini çeker
        private async Task<CategoryGetByIdDto> GetCategoryByIdAsync(int categoryId)
        {
            var query = new GetByIdCategoryQuery() { Id = categoryId };
            var category = await mediator.Send(query);
            return category;
        }
    }
}
