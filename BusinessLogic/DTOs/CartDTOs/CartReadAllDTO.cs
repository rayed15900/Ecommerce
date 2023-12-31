﻿using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IDTOs;
using Models;

namespace BusinessLogic.DTOs.CartDTOs
{
    public class CartReadAllDTO : IDTO
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public string IpAddress { get; set; }
        public bool IsGuest { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        // Foreign Key
        public int UserId { get; set; }
    }
}
