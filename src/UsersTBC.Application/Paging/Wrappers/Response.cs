using UsersTBC.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsersTBC.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            status = ResultStatus.SUCCESS;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public string status { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
