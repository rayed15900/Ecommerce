using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.DiscountDTOs
{
    public class DiscountProductReadDTO : IDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
