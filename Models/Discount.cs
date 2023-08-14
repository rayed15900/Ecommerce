using Models.Base;

namespace Models
{
    public class Discount : BaseModel
    {
        public string Name { get; set; }
        public double Percent { get; set; }
        public bool Active { get; set; }

        // Navigation Property  
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
