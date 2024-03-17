using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ApiBeginner.Filters
{
    public class SensitiveFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Sensitive Action executed!!!!!!!!!!!!!!!!!");

        }
       
    }
}
