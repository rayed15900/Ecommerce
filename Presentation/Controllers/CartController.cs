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

        [HttpGet("ReadAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cart>>> ReadAll()
        {
            var data = await _cartService.CartReadAllAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
