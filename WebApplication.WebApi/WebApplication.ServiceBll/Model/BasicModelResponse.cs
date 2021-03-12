using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.ServiceBll.Model
{
    public class BasicModelResponse<T>
    {
        public string Message { get; set; }
        //0-success
        public int Status { get; set; }
        //0-success
        public int ErrCode { get; set; }
        public int Count { get; set; }
        public List<T> Data { get; set; }

        

        public static BasicModelResponse<T> GetSuccessEntity(List<T> response)
        {
            return new BasicModelResponse<T>()
            {
                Data = response,
                Status = 0,
                ErrCode = 0,
                Count = response.Count,
                Message = "success"
            };
            
        }

        public static BasicModelResponse<T> GetFailedEntity(List<T> response, string Message, int Status, int ErrCode)
        {
            return new BasicModelResponse<T>()
            {
                Data = response,
                Status = Status,
                ErrCode = ErrCode,
                Count = response.Count,
                Message = Message
            };
        }
    }
}
