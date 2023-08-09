using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IServices;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CartItemValidators
{
    public class CartItemUpdateDTOValidator : AbstractValidator<CartItemUpdateDTO>
    {
        private readonly ICartItemService _cartItemService;
        public CartItemUpdateDTOValidator(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId required");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required")
                .MustAsync(QuantityExceed).WithMessage("Quantity cannot exceed");
        }
        private async Task<bool> QuantityExceed(CartItemUpdateDTO dto, int quantity, CancellationToken cancellationToken)
        {
            return await _cartItemService.IsQuantityExceedAsync(dto.ProductId, quantity);
        }
    }
}
