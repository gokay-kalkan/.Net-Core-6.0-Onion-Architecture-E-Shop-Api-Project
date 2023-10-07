

using Application.Dtos.UserDtos;
using Application.Exceptions;
using Application.FluentValidations.ProductFluentValidations;
using Application.FluentValidations.UserFluentValidations;
using Application.IdentityServices;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<CreateUserDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }

        public string Password { get; set; }
    }

    public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, CreateUserDto>
    {
        private readonly IUserService service;
        private readonly IMapper mapper;

        public CreateUserCommandHandle(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            //fluent valdiation
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .Select(group => $"{group.Key}: {string.Join(", ", group.Select(error => error.ErrorMessage))}")
                    .ToList();
               
                throw new AppException((int)HttpStatusCode.BadRequest, "Doğrulama hatası", validationErrors);
            }
            var mapping = mapper.Map<CreateUserDto>(request);
            await service.Create(mapping);
            return mapping;

        }
    }
}
