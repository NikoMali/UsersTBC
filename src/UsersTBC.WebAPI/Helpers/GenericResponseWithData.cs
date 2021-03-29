using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersTBC.WebAPI.Helpers
{
    public class GenericResponseWithData<T> : GenericResponse
    {
        private T _data;

        public T data
        {
            get { return _data; }
            set { _data = value; }
        }
       

        public GenericResponseWithData(T data, bool success, string message)
        {
            this.success = success;
            this.data = data;
            this.message = message;
        }
        public GenericResponseWithData(T data, bool success)
        {
            this.success = success;
            this.data = data;
            this.message = message;
        }

    }
}
