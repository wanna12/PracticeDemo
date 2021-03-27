using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication.Common.model;
using WebApplication.Common.util;
using WebApplication.ServiceBll.Model;
using WebApplication.WebApi.Models;

namespace WebApplication.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        public BasicModelResponse<int> testMethod()
        {
            List<int> resList = new List<int>();
            try
            {
                int x = int.Parse("100");
                resList.Add(x);
                return BasicModelResponse<int>.GetSuccessEntity(resList);
            }
            catch (Exception e)
            {
               // LogHelper.Current.WriteErrorLog(model.inputStr, e);
                return BasicModelResponse<int>.GetFailedEntity(resList, "执行失败", -1, -1);
            }
        }

        [HttpGet]
        public BasicModelResponse<string> getTestMethod([FromBody]TestMethodRequest model)
        {
            return new BasicModelResponse<string>(){Data = new List<string>(){"hello"}};
        }

        public BasicModelResponse<LoginResponse> getSession()
        {
            string session = Guid.NewGuid().ToString();
            SimpleCacheHelper<string>.AddCache("session",session);
            
            return new BasicModelResponse<LoginResponse>()
                {Data = new List<LoginResponse>() {new LoginResponse() {session = session}}};
        }
    }
}
