using GS_Shops.Db;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GC_Shops.Controllers.Items.Actions
{
    public class ItemUpdate
    {
        public class Command : IRequest
        {
            [FromBody]
            public string Name { get; set; }

            [FromBody]
            public int StackSize { get; set; }

            [FromRoute(Name = "id")]
            public string Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly FeaturesDbContext _dbContext;
            public Handler(FeaturesDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _dbContext.Items.SingleAsync(i => i.ItemId == request.Id);
                item.Update(request.Name, request.StackSize);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
