using AutoMapper;
using MediatR;
using Mobiroller.Commands.Models;
using Mobiroller.Common;
using Mobiroller.Data.Entity;
using Mobiroller.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mobiroller.Commands.Handlers
{
    public class ImportIncidentsCommandHandler : IRequestHandler<ImportIncidentsCommand>
    {
        private readonly IIncidentService _incidentService;
        private readonly IMobirollerLocalization _localization;
        private readonly IMapper _mapper;

        public ImportIncidentsCommandHandler(
            IIncidentService incidentService,
            IMobirollerLocalization localization,
            IMapper mapper)
        {
            _incidentService = incidentService ?? throw new ArgumentNullException(nameof(incidentService));
            _localization = localization ?? throw new ArgumentNullException(nameof(localization));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(ImportIncidentsCommand request, CancellationToken cancellationToken)
        {
            var locale = _localization.GetLocalization();

            var jsonSerializer = new JsonSerializer();

            using var streamReader = new StreamReader(request.File.OpenReadStream());
            using var jsonTextReader = new JsonTextReader(streamReader);

            var importIncidents = jsonSerializer.Deserialize<List<ImportIncidentsModel>>(jsonTextReader);

            var incidents = _mapper.Map<List<Incident>>(importIncidents);

            incidents.ForEach(x => x.LocaleId = locale.Id);

            await _incidentService.AddIncidentAsync(incidents);

            return Unit.Value;
        }
    }
}