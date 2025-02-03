using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Vidya.API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Handle exception here
            context.Result = new ObjectResult("An error occurred.")
            {
                StatusCode = 500
            };
        }
    }
}
