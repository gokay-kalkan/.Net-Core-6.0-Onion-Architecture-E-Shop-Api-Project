using Application.Dtos.CartDtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Carts.Queries
{
    public class ListCartQuery : IRequest<List<CartListDto>>
    {

    }

    public class ListCartQueryHandle : IRequestHandler<ListCartQuery, List<CartListDto>>
    {
        private readonly ICartRepository repository;

        private readonly IMapper mapper;

        public ListCartQueryHandle(ICartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CartListDto>> Handle(ListCartQuery request, CancellationToken cancellationToken)
        {
          
            var cartList = repository.List(x => x.Status == true);

          
            var mapping = mapper.Map<List<CartListDto>>(cartList);

            return mapping;
        }
    }
}