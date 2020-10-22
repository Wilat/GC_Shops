using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GC_Shops.Controllers.Shops.Actions.Items;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC_Shops.Controllers.Shops
{
    [ApiController]
    public class ShopsItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Guid _shopId;

        public ShopsItemsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("/api/shops/{ShopId}/items/{ItemId}")]
        public async Task<IActionResult> AddOrUpdateItem(AddOrUpdateItem.Command command, CancellationToken cancellationToken)
        {
            command.ItemId = Request.RouteValues[nameof(Actions.Items.AddOrUpdateItem.Command.ItemId)].ToString();
            command.ShopId = new Guid(Request.RouteValues[nameof(Actions.Items.AddOrUpdateItem.Command.ShopId)].ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
