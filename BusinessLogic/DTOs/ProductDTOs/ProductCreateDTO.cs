using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.ProductDTOs
{
    public class ProductCreateDTO : IDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        public int InventoryId { get; set; }
        public int DiscountId { get; set; }
    }
}
