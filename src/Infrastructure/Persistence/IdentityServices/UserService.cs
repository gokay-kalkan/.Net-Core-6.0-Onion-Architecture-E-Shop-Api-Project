

using Application.Dtos.UserDtos;
using Application.Exceptions;
using Application.FluentValidations.UserFluentValidations;
using Application.IdentityServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.ApiResponse;
using System.Net;

namespace Persistence.IdentityServices
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;


        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;

            _mapper = mapper;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateUserDto> Create(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            IdentityResult identityResult = null;

            var existingUserByEmail = await _userManager.FindByNameAsync(user.Email);
            if (existingUserByEmail != null)
            {
                var emailError = new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = $"{user.Email} adresi zaten kullanılıyor."
                };
                if (identityResult == null)
                {
                    identityResult = IdentityResult.Failed(new IdentityError[] { emailError }); // İlk hatayı eklerken IdentityResult nesnesi null ise yeni bir nesne oluştur
                }
                else
                {
                    identityResult = IdentityResult.Failed(identityResult.Errors.Concat(new IdentityError[] { emailError }).ToArray()); // Daha önce hata eklemesi yapıldıysa mevcut hatalara yeni hatayı ekler
                }
            }

            // Kullanıcı adı kontrolü
            var existingUserByUsername = await _userManager.FindByNameAsync(user.UserName);
            if (existingUserByUsername != null)
            {
                var usernameError = new IdentityError
                {
                    Code = "DuplicateUserName",
                    Description = $"{user.UserName} kullanıcı adı zaten kullanılıyor."
                };

                if (identityResult == null)
                {
                    identityResult = IdentityResult.Failed(new IdentityError[] { usernameError }); // İlk hatayı eklerken IdentityResult nesnesi null ise yeni bir nesne oluştur
                }
                else
                {
                    identityResult = IdentityResult.Failed(identityResult.Errors.Concat(new IdentityError[] { usernameError }).ToArray()); // Daha önce hata eklemesi yapıldıysa mevcut hatalara yeni hatayı ekler
                }
            }

            if (identityResult != null && identityResult.Errors.Any())
            {
                throw new AppException(404, "Kullanıcı Kayıt Edilemedi", identityResult.Errors.Select(error => error.Description).ToList());
            }

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                // Hata işleme

                throw new AppException(404, "Kullanıcı Kayıt Edilemedi", errors);
            }

            return createUserDto;
        }



        public async Task<LoginUserDto> Login(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (user == null)
            {
                throw new AppException(404, "Kullanıcı bulunamadı.");
            }
            // Şifre kontrolü
            var passwordIsValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (!passwordIsValid)
            {
                throw new AppException(400, "Şifreler uyuşmuyor ");
            }

            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginUserDto.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new AppException(401, "Giriş başarısız. Lütfen email ve şifrenizi kontrol edin.");
            }

            var mappedUser = _mapper.Map<User>(loginUserDto);

            _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id);

            return loginUserDto;
        }

    }
}