

using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetByIdCategoryQuery:IRequest<CategoryGetByIdDto>
    {
        public int Id { get; set; }
       
    }

    public class GetByIdCategoryQueryHandle : IRequestHandler<GetByIdCategoryQuery, CategoryGetByIdDto>
    {
        private readonly ICategoryRepository repository;

        private readonly IMapper mapper;

        public GetByIdCategoryQueryHandle(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

       
        public async Task<CategoryGetByIdDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            Category category = repository.GetById(request.Id);

            if (category == null)
            {
                throw new AppException(404, "Kategori Bulunamadı");
            }

            CategoryGetByIdDto categoryGetByIdDto = mapper.Map<CategoryGetByIdDto>(category);

          
            var subCategories = repository.List(x => x.TopCategoryId == category.Id);
            categoryGetByIdDto.SubCategories = mapper.Map<List<CategoryListDto>>(subCategories);

            return categoryGetByIdDto;
        }

    }



}
