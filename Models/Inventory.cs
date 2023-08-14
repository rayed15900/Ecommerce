using Models.Base;

namespace Models
{
    public class Inventory : BaseModel
    {
        public int Quantity { get; set; }

        // Navigational Property
        public virtual Product Product { get; set; }
    }
}
