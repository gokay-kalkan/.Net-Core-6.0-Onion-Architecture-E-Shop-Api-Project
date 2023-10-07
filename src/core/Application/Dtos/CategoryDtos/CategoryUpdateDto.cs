

namespace Application.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public int? TopCategoryId { get; set; }
    }
}
