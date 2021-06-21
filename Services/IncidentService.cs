using Mobiroller.Data;
using Mobiroller.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiroller.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IRepository<int, Incident> _repository;

        public IncidentService(IRepository<int, Incident> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task AddIncidentAsync(IEnumerable<Incident> incidents)
        {
            return _repository.AddRange(incidents);
        }

        public Task<List<Incident>> GetIncidentsAsync()
        {
            return _repository.GetAll();
        }
    }
}