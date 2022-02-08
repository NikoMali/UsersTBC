using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using UsersTBC.Domain;
using UsersTBC.Domain.Localize;
using UsersTBC.Infrastructure.Data;
using UsersTBC.Infrastructure.Helpers;

namespace UsersTBC.Infrastructure.Helpers
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter where T : class, IEntity
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly DataContext _context;
        public ValidateEntityExistsAttribute(
            DataContext context,
            IStringLocalizer<Resource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id = 0;
            if (context.ActionArguments.ContainsKey("Id"))
            {
                id = (int)context.ActionArguments["Id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad Id parameter");
                return;
            }
            var entity = _context.Set<T>().SingleOrDefault(x => x.Id.Equals(id));
            if (entity == null)
            {
                
                context.Result = new OkObjectResult(new { result = _localizer["NotFound"].Value });
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
