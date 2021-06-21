using Mobiroller.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiroller.Services
{
    public interface IIncidentService
    {
        Task AddIncidentAsync(IEnumerable<Incident> incidents);

        Task<List<Incident>> GetIncidentsAsync();
    }
}