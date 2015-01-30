using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fluentx.Mvc
{
    public static class Extensions
    {
        public static RedirectAndPostActionResult RedirectAndPost(this Controller controller, string url, Dictionary<string, object> postData)
        {
            return new RedirectAndPostActionResult(url, postData);
        }
    }
}
