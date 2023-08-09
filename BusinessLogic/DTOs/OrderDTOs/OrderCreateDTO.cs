using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.OrderDTOs
{
    public class OrderCreateDTO : IDTO
    {
        

        // Foreign Key
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingDetailId { get; set; }
    }
}
