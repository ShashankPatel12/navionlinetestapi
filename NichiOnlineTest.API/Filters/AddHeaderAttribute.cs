using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NichiOnlineTest.API.Filters
{
    public class AddHeaderAttribute : ResultFilterAttribute
    {
        private readonly bool _isNeeded;
        private readonly string _value;

        public AddHeaderAttribute(bool isNeeded)
        {
            _isNeeded = isNeeded;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (_isNeeded)
            {
                context.HttpContext.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                context.HttpContext.Response.Headers.Add("Pragma", "no-cache");
                context.HttpContext.Response.Headers.Add("Expires", "0");
                base.OnResultExecuting(context);
            }
        }
    }
}
