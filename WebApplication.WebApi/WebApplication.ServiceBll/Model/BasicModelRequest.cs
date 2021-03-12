using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.WebApi.Models
{
    public class BasicModelRequest
    {
        public string c_ip { get; set; }
        public string s_ip { get; set; }
        public string o_time { get; set; }
        public string version { get; set; }
        public string c_port { get; set; }
    }
}