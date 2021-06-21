using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mobiroller.Controllers
{
    //[NonController]
    public class BaseMobirollerController : ControllerBase
    {
        public Lazy<IMediator> Mediator => new(HttpContext.RequestServices.GetService<IMediator>);

        [NonAction]
        public ActionResult Single<T>(T data)
        {
            return data is null ? NotFound() : Ok(data);
        }

        [NonAction]
        public ActionResult Collection<T>(T data)
        {
            return data is null ? NotFound() : Ok(data);
        }
    }
}