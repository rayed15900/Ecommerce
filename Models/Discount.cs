using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class Discount : BaseModel
    {
        public string Name { get; set; }
        public double Percent { get; set; }
        public bool Active { get; set; }

        // Navigation Property  
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

        public Discount()
        {
            Products = new List<Product>();
        }
    }
}
