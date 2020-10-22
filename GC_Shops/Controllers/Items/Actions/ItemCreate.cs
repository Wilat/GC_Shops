using GC_Shops.Features.Implementations.ItemComponent;
using GS_Shops.Db;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GC_Shops.Controllers.Items.Actions
{
    public class ItemCreate
    {
        public class Command : IRequest
        {
            [FromBody]
            public string Id { get; set; }

            [FromBody]
            public string Name { get; set; }

            [FromBody]
            public int StackSize { get; set; }
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
               _dbContext.Items.Add(new Item(request.Id, request.Name, request.StackSize));
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
