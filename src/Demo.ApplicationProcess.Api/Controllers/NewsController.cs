using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.ApplicationProcess.Api.Models;
using Demo.ApplicationProcess.Api.Validations;
using Demo.ApplicationProcess.Domain.Dtos;
using Demo.ApplicationProcess.Domain.Infrastructure;
using Demo.ApplicationProcess.Domain.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ApplicationProcess.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        /// <summary>
        /// Front service instance
        /// </summary>
        private readonly INewsService _service;

        /// <summary>
        /// Pagination record size
        /// </summary>
        private const int PageSize = 10;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        // GET: api/News
        [HttpGet]
        [Produces(typeof(List<NewsDto>))]
        public async Task<IActionResult> GetAllNews([FromQuery] FilterModel filter = default)
        {
            if (string.IsNullOrWhiteSpace(filter.Search))
                filter.Search = "";

            var entities = await _service.GetAllNewsAsync(filter.Search, filter.Sort, filter.Order, PageSize, filter.Page);

            return Ok(entities);
        }

        // GET: api/News/1
        [HttpGet("{id}")]
        [Produces(typeof(NewsDto))]
        public async Task<IActionResult> GetNews([FromRoute] int id)
        {

            var entity = await _service.GetNewsByIdAsync(id);

            if (entity == null)
            {
                return StatusCode(404, $"Entity#{id} not found");
            }

            return Ok(entity);
        }

        // POST: api/News
        [HttpPost]
        [Produces(typeof(NewsDto))]
        public async Task<IActionResult> PostNews([FromBody] NewsModel model)
        {

            var validator = await new NewsValidation().ValidateAsync(model);

            validator.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _service.AddNewsAsync(model);

            return CreatedAtAction("GetNews", new { id = entity.Id }, entity);
        }

        // PUT: api/News
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNews([FromRoute] int id, [FromBody] NewsModel model)
        {

            var validator = await new NewsValidation().ValidateAsync(model);

            validator.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            await _service.UpdateNewsAsync(id, model);

            return Ok();
        }

        // DELETE: api/News
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews([FromRoute] int id)
        {
            await _service.DeleteNewsAsync(id);

            return Ok();
        }

    }
}
