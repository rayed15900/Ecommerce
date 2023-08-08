using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IServices;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CartItemValidators
{
    public class CartItemCreateDTOValidator : AbstractValidator<CartItemCreateDTO>
    {
        private readonly ICartItemService _cartItemService;
        public CartItemCreateDTOValidator(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId required")
                .MustAsync(DuplicateProduct).WithMessage("Product already exists");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required")
                .MustAsync(QuantityExceed).WithMessage("Quantity cannot exceed");
        }
        private async Task<bool> QuantityExceed(CartItemCreateDTO dto, int quantity, CancellationToken cancellationToken)
        {
            return await _cartItemService.IsQuantityExceedAsync(dto.ProductId, quantity);
        }
        private async Task<bool> DuplicateProduct(int productId, CancellationToken cancellationToken)
        {
            return await _cartItemService.IsDuplicateProductAsync(productId);
        }
    }
}
