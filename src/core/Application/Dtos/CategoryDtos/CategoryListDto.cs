namespace Application.Dtos.CategoryDtos
{

    public class CategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TopCategoryId { get; set; }
        public string TopCategoryName { get; set; }

        public List<CategoryListDto> SubCategories { get; set; }
    }
}


