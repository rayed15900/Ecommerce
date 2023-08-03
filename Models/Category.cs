using Models.Base;

namespace Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        // Navigation Property  
        public ICollection<Product> Products { get; set;}
    }
}
