using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.WebApi.Models;

namespace WebApplication.ServiceBll.Model
{
    public class TestMethodRequest:BasicModelRequest
    {
        public string inputStr { get; set; }
    }
}
