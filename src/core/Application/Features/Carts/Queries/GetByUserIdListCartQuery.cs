
using Application.Dtos.CartDtos;
using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Features.Categories.Queries;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Carts.Queries
{
    public class GetByUserIdListCartQuery:IRequest<List<CartGetByUserIdListDto>>
    {
        public string UserId { get; set; }
    }

    public class GetByUserIdCartQueryHandle : IRequestHandler<GetByUserIdListCartQuery, List<CartGetByUserIdListDto>>
    {
        private readonly ICartRepository repository;

        private readonly IMapper mapper;

        public GetByUserIdCartQueryHandle(ICartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CartGetByUserIdListDto>> Handle(GetByUserIdListCartQuery request, CancellationToken cancellationToken)
        {
            var cart = repository.GetUserIdCart(request.UserId);

            if (cart == null)
            {
                throw new AppException(404, "Sepet Bulunamadı");
            }

           var cartUserList= mapper.Map<List<CartGetByUserIdListDto>>(cart);

            return cartUserList;
        }

    }
}
