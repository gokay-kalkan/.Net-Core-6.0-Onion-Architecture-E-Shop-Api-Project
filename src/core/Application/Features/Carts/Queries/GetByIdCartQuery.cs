
using Application.Dtos.CartDtos;
using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Features.Categories.Queries;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Carts.Queries
{
    public class GetByIdCartQuery : IRequest<CartGetByIdDto>
    {
        public int Id { get; set; }
    }



    
    public class GetByIdCartQueryHandle : IRequestHandler<GetByIdCartQuery, CartGetByIdDto>
    {
        private readonly ICartRepository repository;

        private readonly IMapper mapper;

        public GetByIdCartQueryHandle(ICartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CartGetByIdDto> Handle(GetByIdCartQuery request, CancellationToken cancellationToken)
        {
            
            var cart = repository.GetById(request.Id);

            if (cart == null)
            {
                throw new AppException(404, "Sepet Bulunamadı");
            }

            CartGetByIdDto cartGetByIdDto = mapper.Map<CartGetByIdDto>(cart);
            return cartGetByIdDto;
        }
    }
}


