﻿using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.DiscountDTOs
{
    public class DiscountReadByIdDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Percent { get; set; }
        public bool Active { get; set; }
        public ICollection<DiscountProductReadDTO> Products { get; set; }
    }
}
