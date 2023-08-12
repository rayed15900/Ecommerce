using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class Inventory : BaseModel
    {
        public int Quantity { get; set; }

        // Navigational Property
        [JsonIgnore]
        public virtual Product Inventory_Product { get; set; }
    }
}
