
using Application.Dtos.CartDtos;
using Application.Dtos.CategoryDtos;
using Application.Features.Carts.Commands;
using Application.Features.Carts.Queries;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Shared.ApiResponse;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("CartList")]

        public async Task<IActionResult> GetCarts()
        {
            var query = new ListCartQuery();
            var carts = await _mediator.Send(query);
            return Ok(new Response<List<CartListDto>>(200, carts, "Sepet başarıyla getirildi."));
        }

        [HttpGet("CartGetOne/{id}")]
        public async Task<IActionResult> CartGetOne(int id)
        {
            var query = new GetByIdCartQuery() { Id = id };
            var cart = await _mediator.Send(query);

            return Ok(new Response<CartGetByIdDto>(200, cart, "Sepet başarıyla getirildi."));
        }

        [HttpGet("CartUserGetOne/{id}")]
        public async Task<IActionResult> CartUserGetOne(string id)
        {
            var query = new GetByUserIdListCartQuery() { UserId = id };
            var usercarts = await _mediator.Send(query);

            return Ok(new Response <List<CartGetByUserIdListDto>>(200, usercarts, "Sepet başarıyla getirildi."));
        }

        [HttpPost("create/{productid}")]
       
        public async Task<IActionResult> CreateCart( CreateCartCommand command, int productid)
        {

              command.ProductId = productid;
            
              var result = await _mediator.Send(command);
            
             return Ok(new Response<CartCreateDto>(201, result,"Sepet Başarıyla Kaydedildi"));
          
        }

        [HttpGet("CartDelete/{id}")]

        public async Task<IActionResult> DeleteCart(int id)
        {
            var command = new DeleteCartCommand { Id = id };
            var result = await _mediator.Send(command);

            return Ok(new Response<CartDeleteDto>(200, result, "Sepet başarıyla silindi."));
        }


        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCategory(UpdateCartCommand command)
        {

            var updatedCartDto = await _mediator.Send(command);

            return Ok(new Response<CartUpdateDto>(200, updatedCartDto, "Sepet Adet Miktarı Başarıyla Güncellendi"));
        }


    }
}
