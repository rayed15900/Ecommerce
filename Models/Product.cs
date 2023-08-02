using Models.Base;

namespace Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        // Foreign Key
        public string CategoryId { get; set; }
    }
}
