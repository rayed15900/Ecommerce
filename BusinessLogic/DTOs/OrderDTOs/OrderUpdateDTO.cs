using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.OrderDTOs
{
    public class OrderUpdateDTO : IUpdateDTO
    {
        public int Id { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingDetailId { get; set; }
    }
}
