using Core.Dtos;
using Core.Exceptions;
using Core.Services.Contracts;
using FarmacityTest.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FarmacityTest.Controllers
{
    public class PrincipalController : BaseController
    {
        private readonly IProductService _productService;

        public PrincipalController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);  // Asegúrate de devolver la respuesta correctamente.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);  // Devuelve el error si ocurre alguna excepción.
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                return Ok(product);
            }
            catch (BusinessNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Ocurrió un error interno al procesar la solicitud", Details = ex.Message });
            }
        }

        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> CreateOrUpdateProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null");
            }

            if (string.IsNullOrWhiteSpace(productDto.Name))
                return BadRequest("El nombre del producto es obligatorio.");

            if (string.IsNullOrWhiteSpace(productDto.Barcode?.Code))
                return BadRequest("El código de barras es obligatorio.");

            try
            {
                var product = await _productService.CreateOrUpdateProductAsync(productDto);
                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Conflicto por duplicado de código de barras
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Producto no encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _productService.DeleteAsync(id);
            if (deletedProduct == null)
            {
                return NotFound();
            }

            return NoContent();  // 204 No Content, ya que solo se ha actualizado la entidad
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetActiveProducts()
        {
            var activeProducts = await _productService.GetActiveProducts();
            return Ok(activeProducts);
        }

    }

}
