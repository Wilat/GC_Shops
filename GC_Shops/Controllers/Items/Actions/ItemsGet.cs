﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using GC_Shops.Features.ItemComponent;
using GS_Shops.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GC_Shops.Controllers.Items.Actions
{
    public class ItemsGet
    {
        public class Request : IRequest<ViewModel[]> { }

        public class ViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int StackSize { get; set; }
        }

        public class ViewModelProfile : Profile
        {
            public ViewModelProfile()
            {
                CreateMap<Item, ViewModel>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.ItemId));
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
                (await _dbContext.Items
                    .ProjectTo<ViewModel>(_mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken))
                    .OrderBy(i => Item.IdToString(i.Id))
                    .ToArray();
        }
    }
}
