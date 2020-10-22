using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GC_Shops.Controllers.Items.Actions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC_Shops.Controllers.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreate.Command command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ItemUpdate.Command command, CancellationToken cancellationToken)
        {
            command.Id = Request.RouteValues[nameof(ItemUpdate.Command.Id)].ToString();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new ItemsGet.Request(), cancellationToken));
    }
}
