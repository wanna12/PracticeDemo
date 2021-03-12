using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApplication.Common.util;
using WebApplication.WebApi.Models;

namespace WebApplication.WebApi.Filter
{
    public class LogFilter:ActionFilterAttribute
    {
        private static string key = "_action_excute_";
        private static string inparamName = "model";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Stopwatch stopwatch = new Stopwatch();
            actionContext.Request.Properties[key] = stopwatch;
            stopwatch.Start();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            long excuteTime = 0;
            string inparam = "";
            string outparam = "";
            string func = actionExecutedContext.Request.RequestUri.AbsolutePath;
            if (actionExecutedContext.Exception == null)
            {
                
                if (actionExecutedContext.Request.Properties.ContainsKey(key))
                {
                    Stopwatch stopwatch = actionExecutedContext.Request.Properties[key] as Stopwatch;
                    if (stopwatch != null)
                    {
                        stopwatch.Stop();
                        excuteTime = stopwatch.ElapsedMilliseconds;

                    }

                }
                
                if (actionExecutedContext.ActionContext.ActionArguments.ContainsKey(inparamName))
                {
                    object obj = actionExecutedContext.ActionContext.ActionArguments[inparamName];
                    inparam = JsonConvert.SerializeObject(obj);
                    BasicModelRequest request=new BasicModelRequest();//基础入参
                    ExtensionHelper.CopyPropertiesTo(obj,request,false);
                    
                }

                HttpContent responseContent = actionExecutedContext.Response.Content;
                object responseObj =
                    responseContent.GetType().GetProperty("Value")?.GetValue(responseContent);
                outparam = JsonConvert.SerializeObject(responseObj);
                LogHelper.Current.WriteInfoLog(new LogEnt(func,inparam,outparam,excuteTime));
            }
            else
            {
                LogHelper.Current.WriteErrorLog(new LogEnt(func,inparam,outparam,0),actionExecutedContext.Exception );
            }
        }
    }
}