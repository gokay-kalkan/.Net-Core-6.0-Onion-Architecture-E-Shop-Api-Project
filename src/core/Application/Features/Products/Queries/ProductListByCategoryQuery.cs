using Application.Dtos.ProductDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System.Security.Cryptography.X509Certificates;

namespace Application.Features.Products.Queries
{
    public class ProductListByCategoryQuery : IRequest<List<ProductListByCategoryDto>>
    {

        public int CategoryId { get; set; }
    }

    public class ProductListByCategoryQueryHandle : IRequestHandler<ProductListByCategoryQuery, List<ProductListByCategoryDto>>
    {
        private readonly IProductRepository repository;

        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public ProductListByCategoryQueryHandle(IProductRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<List<ProductListByCategoryDto>> Handle(ProductListByCategoryQuery request, CancellationToken cancellationToken)
        {
            var productList = repository.ProductListByCategory(request.CategoryId);


            if (productList.Count == 0) { throw new AppException(404, "Ürün Kaydı Bulunamadı."); }

            var mapping = mapper.Map<List<ProductListByCategoryDto>>(productList);


            return mapping;
        }


    }
}
