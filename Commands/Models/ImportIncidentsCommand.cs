using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mobiroller.Commands.Models
{
    public class ImportIncidentsCommand : IRequest
    {
        public IFormFile File { get; set; }
    }
}