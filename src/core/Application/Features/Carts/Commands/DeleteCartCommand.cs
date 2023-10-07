

using Application.Dtos.CartDtos;
using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Carts.Commands
{
    public class DeleteCartCommand:IRequest<CartDeleteDto>
    {
        public int Id { get; set; }
    }

    public class DeleteCartCommandHandle : IRequestHandler<DeleteCartCommand, CartDeleteDto>
    {
        private readonly ICartRepository repository;

        private readonly IMapper mapper;

        public DeleteCartCommandHandle(ICartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<CartDeleteDto> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {

            var cart = repository.GetById(request.Id);
            if (cart == null)
            {
                throw new AppException(404, "Sepet Bulunamadı");
            }
            cart.Status = false;
            repository.Update(cart);

            var dto = mapper.Map<CartDeleteDto>(cart);

            return Task.FromResult(dto);
        }
    }
}
