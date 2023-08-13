using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.PaymentDTOs
{
    public class PaymentReadDTO : IDTO
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
