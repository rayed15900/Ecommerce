using BusinessLogic.DTOs.Interfaces;

namespace BusinessLogic.DTOs.DiscountDTOs
{
    public class DiscountUpdateDTO : IUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Percent { get; set; }
        public bool Active { get; set; }
    }
}
