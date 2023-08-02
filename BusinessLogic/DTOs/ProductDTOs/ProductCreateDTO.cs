using BusinessLogic.DTOs.Interfaces;

namespace BusinessLogic.DTOs.ProductDTOs
{
    public class ProductCreateDTO : IDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
    }
}
