using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CategoryDTOs
{
    public class CategoryReadDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
