using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobiroller.Commands.Models;
using Mobiroller.DTOs;
using Mobiroller.Queries.Models;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Mobiroller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IncidentsController : BaseMobirollerController
    {
        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] ImportIncidentsCommand command)
        {
            await Mediator.Value.Send(command);

            return Accepted();
        }

        [HttpGet]
        public async Task<ActionResult<List<IncidentDto>>> GetIncident()
        {
            return Collection(await Mediator.Value.Send(new GetIncidentsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentDto>> GetIncident([FromRoute] GetIncidentQuery query)
        {
            return Single(await Mediator.Value.Send(query));
        }
    }
}