using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        // base --> Result classı

        // Result classının iki parametreli constuctorı çalışır.
        public SuccessResult(string message) : base(true, message) 
        { 
        }
        // Result classının tek parametreli constuctorı çalışır.
        public SuccessResult() : base(true)
        {
        }
    }
}
