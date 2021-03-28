using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using UsersTBC.Domain;
using UsersTBC.Insfrastructure.Helpers;

namespace UsersTBC.Infrastructure.Helpers
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter where T : class, IEntity
    {
        private readonly DataContext _context;
        public ValidateEntityExistsAttribute(DataContext context)
        {
            _context = context;
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
                context.Result = new NotFoundResult();
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
