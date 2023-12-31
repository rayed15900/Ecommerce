﻿using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class CartItem : BaseModel
    {
        public int Quantity { get; set; }

        // Foreign Key
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Cart Cart { get; set; }
    }
}
