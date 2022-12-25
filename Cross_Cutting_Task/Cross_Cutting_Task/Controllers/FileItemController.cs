using Cross_Cutting_Task.FileItems;
using Cross_Cutting_Task.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cross_Cutting_Task.Controllers;

[Route("api/[controller]")]
    public class FileItemController : Controller
    {
        private readonly IFileItemRepository _repository;
        public FileItemController(IFileItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("fileslist")]
        public async Task<IActionResult> GetList()
        {
            if (await _repository.IsEmpty())
                return NoContent();

            return new OkObjectResult(await _repository.GetAllAsync());
        }

        [HttpGet]
        [Route("file/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (await _repository.IsEmpty())
                return NoContent();

            var file = await _repository.GetItemByIdAsync(id);
            return new OkObjectResult(file);
        }

        [HttpPost]
        [Route("addfile")]
        public async Task<IActionResult> PostAsync([FromBody] FileItem item)
        {
            if (item is null)
                return BadRequest();

            await _repository.AddAsync(item);
            return StatusCode(201);
        }
        

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] FileItem item)
        {
            if (await _repository.IsEmpty())
                return NoContent();

            var product = await _repository.GetItemByIdAsync(id);
            if (product is null)
                return NotFound();

            product = item;
            await _repository.UpdateAsync(product);
            return new OkObjectResult(product);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _repository.IsEmpty())
                return NoContent();

            var product = await _repository.GetItemByIdAsync(id);
            if (product is null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return new OkResult();
        }
    }