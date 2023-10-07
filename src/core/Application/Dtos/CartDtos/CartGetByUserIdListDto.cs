

using Application.Exceptions;
using Application.Features.Carts.Queries;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Dtos.CartDtos
{
    public class CartGetByUserIdListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string ProductImage { get; set; }
        public int ProductId { get; set; }

    }

    
}
