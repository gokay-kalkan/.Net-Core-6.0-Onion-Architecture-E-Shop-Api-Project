

using Application.Dtos.UserDtos;
using Application.IdentityServices;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class LoginUserCommand:IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandle : IRequestHandler<LoginUserCommand, LoginUserDto>
    {
        private readonly IUserService service;
        private readonly IMapper mapper;

        public LoginUserCommandHandle(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var mapping = mapper.Map<LoginUserDto>(request);
            await service.Login(mapping);
            return mapping;

        }
    }
}
