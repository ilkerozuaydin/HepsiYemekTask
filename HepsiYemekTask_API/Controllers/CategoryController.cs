using Business.Abstract;
using Entities.Dtos.Category;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryForAddUpdateDto dto)
        {
            var result = await _categoryService.Add(dto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetCategory), new { id = result.Data.Id }, result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _categoryService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(string id)
        {
            var result = await _categoryService.GetCategory(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(string id, [FromBody] CategoryForAddUpdateDto dto)
        {
            var result = await _categoryService.Update(id, dto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetCategory), new { id = result.Data.Id }, result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet,HttpGet("list"), EnableQuery]
        public async Task<IActionResult> GetCategoryList([FromQuery] CategoryForGetListDto dto)
        {
            var result = await _categoryService.GetCategoryList(dto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}