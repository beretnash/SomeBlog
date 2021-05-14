using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeBlog.Application.Features.Commands.Blogs;
using SomeBlog.Application.Features.Commands.Categories;
using SomeBlog.Application.Features.Queries.Blogs;
using SomeBlog.Application.Filters.Blogs;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SomeBlog.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BlogsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBlogsFilter filter)
        {
            if (filter.IsMine.GetValueOrDefault())
            {
                return Ok(await Mediator.Send(new GetAllPublishedBlogsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
            }

            var query = new GetAllOwnBlogsQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                AuthorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            return Ok(await Mediator.Send(new GetBlogBySlugQuery { Slug = slug }));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetBlogByIdQuery()
            {
                Id = id,
                AuthorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateBlogCommand command)
        {
            command.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateBlogCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            command.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
