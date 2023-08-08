using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.ProductDTOs
{
    public class ProductDetailDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double OriginalPrice { get; set; }
        public double PriceReduced { get; set; }
        public double DiscountedPrice { get; set; }
        public double DiscountPercent { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
