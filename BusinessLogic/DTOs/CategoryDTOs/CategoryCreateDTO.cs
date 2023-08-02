using BusinessLogic.DTOs.Interfaces;
using Models;

namespace BusinessLogic.DTOs.CategoryDTOs
{
    public class CategoryCreateDTO : IDTO
    {
        public string Name { get; set; }
    }
}
