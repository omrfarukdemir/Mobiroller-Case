using MediatR;
using Mobiroller.DTOs;

namespace Mobiroller.Queries.Models
{
    public class GenerateTokenQuery:IRequest<JwtDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}