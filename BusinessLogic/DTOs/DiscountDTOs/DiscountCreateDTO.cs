using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.DiscountDTOs
{
    public class DiscountCreateDTO : IDTO
    {
        public string Name { get; set; }
        public double Percent { get; set; }
        public bool Active { get; set; }
    }
}
