using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.ProductDTOs
{
    public class ProductReadByIdDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double OriginalPrice { get; set; }
        public string DiscountName { get; set; }
        public double PriceReduced { get; set; }
        public double DiscountPercent { get; set; }
        public double FinalPrice { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
