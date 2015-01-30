using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fluentx.Mvc
{
    /// <summary>
    /// This action result posts the specified data dictionary to the specified url, posted data values will be converted using the default string conversion.
    /// </summary>
    public class RedirectAndPostActionResult : ActionResult
    {
        public string Url { get; set; }
        public Dictionary<string, object> PostData { get; set; }
        public RedirectAndPostActionResult(string url, Dictionary<string, object> postData)
        {
            Url = url;
            PostData = postData ?? new Dictionary<string, object>();
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var strHtml = BuildPostForm(Url, PostData);
            context.HttpContext.Response.Write(strHtml);
        }

        private string BuildPostForm(string Url, Dictionary<string, object> PostData)
        {
            string formId = "__PostForm";

            StringBuilder strForm = new StringBuilder();
            strForm.Append(string.Format("<form id=\"{0}\" name=\"{0}\" action=\"{1}\" method=\"POST\">", formId, Url));
            foreach (var item in PostData)
            {
                strForm.Append(string.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\"/>", item.Key, item.Value));
            }
            strForm.Append("</form>");

            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language=\"javascript\">");
            strScript.Append(string.Format("var v{0}=document.{0};", formId));
            strScript.Append(string.Format("v{0}.submit();", formId));
            strScript.Append("</script>");

            return strForm.ToString() + strScript.ToString();
        }

        public static RedirectAndPostActionResult RedirectAndPost(string url, Dictionary<string, object> postData)
        {
            return new RedirectAndPostActionResult(url, postData);
        }
    }
}
