using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // İlk constructor çalıştığında iknici constructorın da çalışmasını istiyoruz (kod tekrarını önlemek için)
        // : this(success) --> bu constructor çalıştığında bu classın (this) diğer constructorunu da çalıştır.
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        // Sadece get metotları olduğu için bu formatta geldi
        public bool Success  { get; }

        public string Message { get; }
    }
}
