using Business.Abstract;
using Entities.Dtos.Product;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductForAddUpdateDto dto)
        {
            var result = await _productService.Add(dto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetProduct), new { id = result.Data.Id }, result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var result = await _productService.GetProduct(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(string id, [FromBody] ProductForAddUpdateDto dto)
        {
            var result = await _productService.Update(id, dto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetProduct), new { id = result.Data.Id }, result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet,HttpGet("list"),EnableQuery]
        public async Task<IActionResult> GetProductList([FromQuery] ProductForGetListDto dto)
        {
            var result = await _productService.GetProductList(dto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}