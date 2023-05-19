using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class ValidationExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                exception.Errors
                    .ToList()
                    .ForEach(e => context.ModelState.AddModelError(e.PropertyName, e.ErrorMessage));

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
