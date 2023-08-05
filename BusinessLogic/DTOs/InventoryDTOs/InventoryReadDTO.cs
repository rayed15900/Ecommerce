﻿using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.InventoryDTOs
{
    public class InventoryReadDTO : IDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
