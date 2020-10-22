using AutoMapper;
using AutoMapper.QueryableExtensions;
using GC_Shops.Features.Implementations.ItemComponent;
using GC_Shops.Features.Implementations.ShopComponent;
using GS_Shops.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GC_Shops.Controllers.Shops.Actions
{
    public class GetShops
    {
        public class Request : IRequest<ViewModel[]> { }

        public class ViewModel
        {
            public string Name { get; set; }
            public Item[] Items { get; set; }
            public Guid Id { get; set; }

            public class Item
            {
                public string Id { get; set; }
                public decimal SellCost { get; set; }
                public uint ItemsInSell { get; set; }
                public decimal BuyCost { get; set; }
                public uint ItemsInBuy { get; set; }
            }
        }

        public class ViewModelProfile : Profile
        {
            public ViewModelProfile()
            {
                CreateMap<Shop, ViewModel>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.ShopId));
                CreateMap<ShopItem, ViewModel.Item>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Item.ItemId));
            }
        }

        public class Handler : IRequestHandler<Request, ViewModel[]>
        {
            private readonly FeaturesDbContext _dbContext;
            private readonly IMapper _mapper;
            public Handler(FeaturesDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ViewModel[]> Handle(Request request, CancellationToken cancellationToken) =>
                (await _dbContext.Shops
                    .Include(s => s.Items)
                        .ThenInclude(si => si.Item)
                    .ProjectTo<ViewModel>(_mapper.ConfigurationProvider)
                    .OrderBy(i => i.Name)
                    .ToArrayAsync(cancellationToken))
                    .Select(shop =>
                    {
                        shop.Items = shop.Items
                            .OrderBy(i => Item.IdToString(i.Id))
                            .ToArray();
                        return shop;
                    })
                    .ToArray();
        }
    }
}
