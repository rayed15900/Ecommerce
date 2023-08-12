using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CategoryDTOs
{
    public class CategoryReadByIdDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryProductReadDTO> Products { get; set; }
    }
}
