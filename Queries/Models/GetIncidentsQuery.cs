using MediatR;
using Mobiroller.DTOs;
using System.Collections.Generic;

namespace Mobiroller.Queries.Models
{
    public class GetIncidentsQuery : IRequest<List<IncidentDto>>
    {
    }
}