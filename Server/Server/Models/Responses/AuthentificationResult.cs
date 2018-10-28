using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.Responses
{
    public class AuthentificationResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        public AuthentificationResult Failed(string msg, string data)
        {
            this.Message = msg;
            this.Data = data;
            this.Status = false;

            return this;
        }

        public AuthentificationResult Success(string msg, string data)
        {
            this.Message = msg;
            this.Data = data;
            this.Status = true;

            return this;
        }

        // Update this class with another method definition of these 2 without parameters.
    }
}
