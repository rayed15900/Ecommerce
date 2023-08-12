using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CategoryDTOs
{
    public class CategoryProductReadDTO : IDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
