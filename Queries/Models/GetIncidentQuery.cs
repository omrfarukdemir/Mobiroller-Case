using MediatR;
using Mobiroller.DTOs;

namespace Mobiroller.Queries.Models
{
    public class GetIncidentQuery : IRequest<IncidentDto>
    {
        public int Id { get; set; }
    }
}