using Models.Base;

namespace Models
{
    public class Inventory : BaseModel
    {
        public int Quantity { get; set; }

        // Navigational Property
        public Product Product { get; set; }
    }
}
