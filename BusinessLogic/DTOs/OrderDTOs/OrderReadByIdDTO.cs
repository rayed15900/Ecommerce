using BusinessLogic.IDTOs;
using Models;

namespace BusinessLogic.DTOs.OrderDTOs
{
    public class OrderReadByIdDTO : IDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public double TotalAmount { get; set; }
        public string Address { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
