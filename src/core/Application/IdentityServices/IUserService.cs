
using Application.Dtos.UserDtos;
using Shared.ApiResponse;

namespace Application.IdentityServices
{
    public interface IUserService
    {
        Task<CreateUserDto> Create(CreateUserDto createUserDto);
        Task<LoginUserDto> Login(LoginUserDto loginUserDto);
    }
}
