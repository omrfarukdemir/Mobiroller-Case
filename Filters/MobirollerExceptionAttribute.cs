using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;

namespace Mobiroller.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MobirollerExceptionAttribute : Attribute, IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public MobirollerExceptionAttribute(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            context.Result = new BadRequestObjectResult(new
            {
                Error = context.Exception.Message,
                StakTrace = _hostEnvironment.IsDevelopment() ? context.Exception.StackTrace : null
            });
        }
    }
}