using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CategoryDTOs
{
    public class CategoryReadAllDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
