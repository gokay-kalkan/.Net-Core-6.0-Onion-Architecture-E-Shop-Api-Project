using Application.Dtos.CategoryDtos;
using Application.Dtos.UserDtos;
using Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiResponse;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var user = await _mediator.Send(command);

            return Ok(new Response<CreateUserDto>(201, user,"Başarıyla Kayıt Oldunuz"));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (LoginUserCommand command)
        {
            var user = await _mediator.Send(command);

            return Ok(new Response<LoginUserDto>(201, user,"Giriş İşlemi Başarılı"));
        }

    }
}

