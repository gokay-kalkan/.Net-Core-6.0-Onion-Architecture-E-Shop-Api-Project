
using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.FluentValidations.CategoryFluentValidations;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand:IRequest<CategoryUpdateDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TopCategoryId { get; set; }
    }

    public class UpdateCategoryCommandHandle : IRequestHandler<UpdateCategoryCommand, CategoryUpdateDto>
    {
        private readonly ICategoryRepository repository;

        private readonly IMapper mapper;

        public UpdateCategoryCommandHandle(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<CategoryUpdateDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            
            var dto = mapper.Map<Category>(request);

           Category updateCategory= await repository.UpdateAsync(dto);

            CategoryUpdateDto updateCategoryDto = mapper.Map<CategoryUpdateDto>(updateCategory);

            return updateCategoryDto;
        }
    }
}
