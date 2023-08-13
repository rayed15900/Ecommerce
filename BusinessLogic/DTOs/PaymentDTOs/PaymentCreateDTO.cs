using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.PaymentDTOs
{
    public class PaymentCreateDTO : IDTO
    {
        public double Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
