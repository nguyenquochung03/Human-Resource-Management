using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HumanResourceManagement.Validations
{
    public class CustomValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var firstError = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault();

                if (firstError != null)
                {
                    var response = new
                    {
                        statusCode = 422,
                        message = firstError.ErrorMessage,
                        data = ""
                    };
                    context.Result = new UnprocessableEntityObjectResult(response);

                }
            }
        }
    }
}
