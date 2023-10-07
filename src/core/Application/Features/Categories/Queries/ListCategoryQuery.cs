

using Application.Dtos.CategoryDtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class ListCategoryQuery : IRequest<List<CategoryListDto>>
    {

    }

    public class ListCategoryQueryHandle : IRequestHandler<ListCategoryQuery, List<CategoryListDto>>
    {
        private readonly ICategoryRepository repository;

        private readonly IMapper mapper;

        public ListCategoryQueryHandle(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CategoryListDto>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
        {
            // Veritabanından kategorileri çek
            var categoryList = repository.List(x => x.Status == true).OrderBy(x => x.Name).ToList();

            // Model mapping
            var mapping = mapper.Map<List<CategoryListDto>>(categoryList);

            // Ağaç yapısını oluştur
            var tree = BuildTree(mapping);

            return tree;
        }

        private List<CategoryListDto> BuildTree(List<CategoryListDto> categories, int? parentId = null)
        {
            return categories
                .Where(c => c.TopCategoryId == parentId)
                .Select(c => new CategoryListDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    TopCategoryId = c.TopCategoryId,
                    // Özyinelemeli olarak alt kategorileri ayarla
                    SubCategories = BuildTree(categories, c.Id)
                })
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
