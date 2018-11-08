using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Response<DataType>
    {
         public bool Status { get; set; }
         public string Message { get; set; }
         public DataType Data { get; set; }

         public Response<DataType> Failed(string message, DataType data)
         {
             this.Message = message;
             this.Data = data;
             this.Status = false;

             return this;
         }

         public Response<DataType> Success(string message, DataType data)
         {
             this.Message = message;
             this.Data = data;
             this.Status = true;

             return this;
         }
    }
}
