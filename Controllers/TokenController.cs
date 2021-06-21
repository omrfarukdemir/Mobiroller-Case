using Microsoft.AspNetCore.Mvc;
using Mobiroller.Queries.Models;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Mobiroller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class TokenController : BaseMobirollerController
    {
        [HttpPost("generate")]
        public async Task<IActionResult> Generate(GenerateTokenQuery query)
        {
            return Ok(await Mediator.Value.Send(query));
        }
    }
}