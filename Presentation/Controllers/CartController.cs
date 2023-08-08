//using BusinessLogic.DTOs.CartDTOs;
//using BusinessLogic.IServices;
//using FluentValidation;
//using Microsoft.AspNetCore.Mvc;
//using Models;
//using System.Net;

//namespace Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartController : ControllerBase
//    {
//        private readonly ICartService _cartService;
//        private readonly IValidator<CartCreateDTO> _cartCreateDtoValidator;
//        private readonly IValidator<CartUpdateDTO> _cartUpdateDtoValidator;

//        public CartController(ICartService cartService, IValidator<CartCreateDTO> cartCreateDtoValidator, IValidator<CartUpdateDTO> cartUpdateDtoValidator)
//        {
//            _cartService = cartService;
//            _cartCreateDtoValidator = cartCreateDtoValidator;
//            _cartUpdateDtoValidator = cartUpdateDtoValidator;
//        }

//        [HttpGet("Read")]
//        public async Task<ActionResult<IEnumerable<Cart>>> Read()
//        {
//            var data = await _cartService.GetAllAsync();
//            return Ok(data);
//        }

//        [HttpGet("Read/{id}")]
//        public async Task<ActionResult<Cart>> ReadById(int id)
//        {
//            var data = await _cartService.GetByIdAsync<Cart>(id);
//            if (data == null)
//            {
//                return NotFound();
//            }
//            return Ok(data);
//        }

//        [HttpPost("Create")]
//        public async Task<ActionResult<Cart>> Create(CartCreateDTO dto)
//        {
//            var validationResult = await _cartCreateDtoValidator.ValidateAsync(dto);

//            if (validationResult.IsValid)
//            {
//                var data = await _cartService.CreateAsync(dto);

//                if (data != null)
//                {
//                    return Ok(new { Msg = "Created", Data = data });
//                }
//                else
//                {
//                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created", Data = data });
//                }
//            }
//            else
//            {
//                var errorMessages = new List<string>();
//                foreach (var error in validationResult.Errors)
//                {
//                    errorMessages.Add(error.ErrorMessage);
//                }
//                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
//            }
//        }

//        [HttpPost("Update")]
//        public async Task<ActionResult<Cart>> Update(CartUpdateDTO dto)
//        {
//            var validationResult = await _cartUpdateDtoValidator.ValidateAsync(dto);

//            if (validationResult.IsValid)
//            {
//                var data = await _cartService.UpdateAsync(dto);
//                if (data != null)
//                {
//                    return Ok(new { Msg = "Updated", Data = data });
//                }
//                else
//                {
//                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated", Data = data });
//                }
//            }
//            else
//            {
//                var errorMessages = new List<string>();
//                foreach (var error in validationResult.Errors)
//                {
//                    errorMessages.Add(error.ErrorMessage);
//                }
//                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
//            }
//        }

//        [HttpPost("Delete/{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            var data = await _cartService.RemoveAsync(id);
//            if (data == null)
//                return NotFound();
//            return Ok(new { Msg = "Deleted", Data = data });
//        }
//    }
//}
