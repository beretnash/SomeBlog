using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeBlog.Application.Features.Commands.Comments;
using SomeBlog.Application.Features.Queries.Comments;
using SomeBlog.Application.Filters.Comments;
using System;
using System.Threading.Tasks;

namespace SomeBlog.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class CommentController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCommentsFilter filter)
        {
            return Ok(await Mediator.Send(new GetAllCommentsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber, BlogId = filter.BlogId }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCommentByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateCommentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateCommentCommand command)
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
            return Ok(await Mediator.Send(new UpdateCommentCommand { Id = id }));
        }
    }
}
