using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Test.Exceptions;

namespace Api.Filters
{
    public class InvalidWordExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is InvalidWordException exception)
            {
                context.ModelState.AddModelError("word", exception.Message);
                var validationProblem = new ValidationProblemDetails(context.ModelState);
                context.Result = new ObjectResult(validationProblem)
                {
                    StatusCode = validationProblem?.Status
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
