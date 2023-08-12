using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.ShippingDetailDTOs
{
    public class ShippingDetailReadByIdDTO : IDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
