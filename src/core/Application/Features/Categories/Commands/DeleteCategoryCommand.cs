

using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand:IRequest<CategoryDeleteDto>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandle : IRequestHandler<DeleteCategoryCommand, CategoryDeleteDto>
    {
        private readonly ICategoryRepository repository;

        private readonly IMapper mapper;

        public DeleteCategoryCommandHandle(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<CategoryDeleteDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            
            var category = repository.GetById(request.Id);
            if (category==null)
            {
                throw new AppException(404, "Kategori Bulunamadı");
            }
            category.Status = false;
            repository.Update(category);

            var dto = mapper.Map<CategoryDeleteDto>(category);

            return Task.FromResult(dto);
        }
    }
}
