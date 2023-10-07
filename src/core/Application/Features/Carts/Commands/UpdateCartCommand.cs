

using Application.Dtos.CartDtos;
using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Carts.Commands
{
    public class UpdateCartCommand:IRequest<CartUpdateDto>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
       
    }

    public class UpdateCartCommandHandle : IRequestHandler<UpdateCartCommand, CartUpdateDto>
    {
        private readonly ICartRepository repository;
        private readonly IProductRepository productRepository;

        private readonly IMapper mapper;

        public UpdateCartCommandHandle(ICartRepository repository, IMapper mapper, IProductRepository productRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.productRepository = productRepository; 
        }
        public async Task<CartUpdateDto> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            // Sepeti ve ürünü al
            var cart =  repository.GetById(request.Id);

            var product =  productRepository.GetById(cart.ProductId);

            // Yeni adet miktarını kontrol et
            if (request.Quantity < 1)
            {
                throw new AppException(500, "Adet miktarı minimum 1 olmalıdır.");
            }

            if (request.Quantity > product.Stock)
            {
                  throw new AppException(500, "Ürün stok miktarını aşan bir adet miktarı giremezsiniz.");
            }

            // Güncelleme işlemini yap
            cart.Quantity = request.Quantity;
            await repository.UpdateAsync(cart);

            // Ürün stok miktarını güncelle
            product.Stock -= request.Quantity;
            await productRepository.UpdateAsync(product);

            // Güncelleme sonucunu dön
            var updatedCartDto = mapper.Map<CartUpdateDto>(cart);
            return updatedCartDto;
        }
    }
}

