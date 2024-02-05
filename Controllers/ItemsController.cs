using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repository;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly ItemRepository _repository = new();
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var items = (await _repository.GetAllAsync())
            .Select(item => item.AsDto());

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreatedItemDto createdItemDto)
        {
            var item = new Item
            {
                Name = createdItemDto.Name,
                Description = createdItemDto.Description,
                Price = createdItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _repository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdatedItemDto updatedItemDto)
        {

            var existingItem = await _repository.GetAsync(id);

            if (existingItem == null) return NotFound();

            existingItem.Name = updatedItemDto.Name;
            existingItem.Description = updatedItemDto.Description;
            existingItem.Price = updatedItemDto.Price;

            await _repository.UpdateAsync(existingItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existingItem = await _repository.GetAsync(id);

            if (existingItem == null) return NotFound();

            await _repository.RemoveAsync(id);
            return NoContent();
        }
    }


}