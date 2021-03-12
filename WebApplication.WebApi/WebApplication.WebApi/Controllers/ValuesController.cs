using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        [HttpPost]
        public BasicModelResponse<int> testMethod(TestMethodRequest model)
        {
            List<int> resList = new List<int>();
            try
            {
                dynamic obj = model.inputStr;
                int x = int.Parse(obj);
                
                resList.Add(x);
                return BasicModelResponse<int>.GetSuccessEntity(resList);
            }
            catch (Exception e)
            {
                return BasicModelResponse<int>.GetFailedEntity(resList, "执行失败", -1, -1);
            }
        }
    }
}
