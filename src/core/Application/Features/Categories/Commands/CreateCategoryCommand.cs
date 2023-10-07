
using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.FluentValidations.CategoryFluentValidations;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Features.Categories.Commands
{
    public class CreateCategoryCommand:IRequest<CategoryCreateDto>
    {
        public string Name { get; set; }
        public int? TopCategoryId { get; set; }
    }

    public class CreateCategoryCommandHandle : IRequestHandler<CreateCategoryCommand, CategoryCreateDto>
    {
        private readonly ICategoryRepository repository;

        private readonly IMapper mapper;

        public CreateCategoryCommandHandle(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CategoryCreateDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            //fluent valdiation
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .Select(group => $"{group.Key}: {string.Join(", ", group.Select(error => error.ErrorMessage))}")
                    .ToList();

                // Doğrulama hatasını yeni AppException ile fırlattık
                throw new AppException((int)HttpStatusCode.BadRequest, "Doğrulama hatası", validationErrors);
            }

            Category category = mapper.Map<Category>(request);
            category.Status = true;

            Category createCategory = await repository.AddAsync(category);

            CategoryCreateDto createCategoryDto = mapper.Map<CategoryCreateDto>(createCategory);
            return createCategoryDto;
        }
    }
}
