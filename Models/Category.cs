using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        // Navigation Property  
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
