using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.OrderDTOs
{
    public class OrderReadAllDTO : IDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingDetailId { get; set; }
    }
}
