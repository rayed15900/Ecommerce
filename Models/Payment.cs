using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class Payment : BaseModel
    {
        public double Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }


        // Navigation Property
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
