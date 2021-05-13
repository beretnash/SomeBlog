using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeBlog.Application.Features.Commands.Categories;
using SomeBlog.Application.Features.Queries.Categories;
using SomeBlog.Application.Filters.Categories;
using System;
using System.Threading.Tasks;

namespace SomeBlog.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class CategoriesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCategoriesFilter filter)
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}
