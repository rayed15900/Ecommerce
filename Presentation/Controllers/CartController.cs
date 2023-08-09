using BusinessLogic.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("Detail")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cart>>> Detail()
        {
            var data = await _cartService.CartDetailAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
