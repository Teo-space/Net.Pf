using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Net.Pf.Application.Filters;


public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    protected readonly ILogger<CustomExceptionFilterAttribute> Logger;

    public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
    {
        Logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        Logger.LogError(context.Exception, context.ActionDescriptor.DisplayName);

        context.Result = Result(
            new ErrorMessage(
                context.Exception.GetType().Name,
                context.Exception.Message,
                context.ActionDescriptor.DisplayName),
            statusCode: 500);

        //context.Result = Result(context.Exception, statusCode: 500);
        context.ExceptionHandled = true;
    }

    JsonResult Result(object o, int statusCode) => new JsonResult(o) { StatusCode = statusCode };

    record ErrorMessage(string exceptionName, string exceptionMessage, string actionName);
}
