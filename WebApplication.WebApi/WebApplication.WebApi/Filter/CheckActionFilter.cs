using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using WebApplication.Common.model;
using WebApplication.Common.util;
using WebApplication.ServiceBll.Model;

namespace WebApplication.WebApi.Filter
{
    public class CheckActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IEnumerable<string> sessions;
            string[] pathStrings = actionContext.Request.RequestUri.AbsolutePath.Split('/');
            string method = pathStrings[pathStrings.Length - 1];
            if (!"getSession".Equals(method))
            {
                if (actionContext.Request.Headers.TryGetValues("em_session", out sessions))
                {
                    string session = sessions.FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(session) ||
                        !session.Equals(SimpleCacheHelper<string>.GetCache("session")))
                    {
                        BasicModelResponse<string> response = new BasicModelResponse<string>()
                        {
                            ErrCode = -1,
                            Status = -2,
                            Data = new List<string>() {"session过期"}
                        };
                        HttpResponseMessage responseMessage = new HttpResponseMessage();
                        responseMessage.Content = new StringContent(JsonConvert.SerializeObject(response));
                        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        actionContext.Response = responseMessage;
                        return;
                    }

                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    BasicModelResponse<string> response = new BasicModelResponse<string>()
                    {
                        ErrCode = -1,
                        Status = -2,
                        Data = new List<string>() {"session过期"}
                    };
                    HttpResponseMessage responseMessage = new HttpResponseMessage();
                    responseMessage.Content = new StringContent(JsonConvert.SerializeObject(response));
                    responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    actionContext.Response = responseMessage;
                }
            }
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var request = actionExecutedContext.Request;
            string[] pathStrings = request.RequestUri.AbsolutePath.Split('/');
            string method = pathStrings[pathStrings.Length - 1];
            if ("getSession".Equals(method))
            {
                //如果调用的getsession，设置response的header
                var response = actionExecutedContext.Response;
                HttpContent responseContent = response.Content;
                string session = SimpleCacheHelper<string>.GetCache("session");
                response.Headers.Add("em_session",session);
                HttpCookie cookie = new HttpCookie("em_session", session);
                cookie.HttpOnly = true;
                cookie.Secure = true;
                cookie.SameSite = SameSiteMode.Lax;
                //cookie.Domain = "192.168.*";
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}