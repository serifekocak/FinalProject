using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        // base --> Result classı

        // Result classının iki parametreli constuctorı çalışır.
        public ErrorResult(string message) : base(false, message)
        {

        }
        // Result classının tek parametreli constuctorı çalışır.
        public ErrorResult() : base(false)
        {

        }
    }
}
