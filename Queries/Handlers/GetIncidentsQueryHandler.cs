using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Mobiroller.Common;
using Mobiroller.Data.Entity;
using Mobiroller.DTOs;
using Mobiroller.Queries.Models;
using Mobiroller.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mobiroller.Queries.Handlers
{
    public class GetIncidentsQueryHandler : IRequestHandler<GetIncidentsQuery, List<IncidentDto>>
    {
        private readonly IMobirollerLocalization _localization;
        private readonly IMemoryCache _cache;
        private readonly IIncidentService _incidentService;
        private readonly IMapper _mapper;

        public GetIncidentsQueryHandler(
            IMobirollerLocalization localization,
            IMemoryCache cache,
            IIncidentService incidentService,
            IMapper mapper)
        {
            _localization = localization ?? throw new ArgumentNullException(nameof(localization));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _incidentService = incidentService ?? throw new ArgumentNullException(nameof(incidentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<IncidentDto>> Handle(GetIncidentsQuery request, CancellationToken cancellationToken)
        {
            var incidents = _cache.Get<List<Incident>>(CacheKeys.Incidents) ??
                                        _cache.Set(CacheKeys.Incidents, await _incidentService.GetIncidentsAsync());

            var locale = _localization.GetLocalization();

            return _mapper.Map<List<IncidentDto>>(incidents?.Where(x => x.LocaleId == locale.Id));
        }
    }
}