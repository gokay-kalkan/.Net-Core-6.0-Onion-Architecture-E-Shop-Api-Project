

using Application.Dtos.CategoryDtos;
using Application.Dtos.ProductDtos;
using Application.Exceptions;
using Application.Features.Categories.Queries;
using Application.Interfaces;
using AutoMapper;

using MediatR;



namespace Application.Features.Products.Queries
{
    public class ListProductQuery : IRequest<List<ProductListDto>>
    {
    }

    public class ListProductQueryHandle: IRequestHandler<ListProductQuery, List<ProductListDto>>
    {
        private readonly IProductRepository repository;

        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public ListProductQueryHandle(IProductRepository repository,IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<List<ProductListDto>> Handle(ListProductQuery request, CancellationToken cancellationToken)
        {
            var productList = repository.List(x => x.Status == true);

       
            if (productList.Count==0) { throw new AppException(404, "Ürün Kaydı Bulunamadı.");}

            var mapping = mapper.Map<List<ProductListDto>>(productList);

          
            return mapping;
        }

        
    }
}