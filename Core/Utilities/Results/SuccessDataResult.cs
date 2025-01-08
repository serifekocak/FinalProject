using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        // Data, true ve message göndermek istediğinde
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }
        // Sadece data ve true göndermek istediğinde
        public SuccessDataResult(T data) : base(data, true)
        {
            
        }
        // Sadece message göndermek istediğinde
        public SuccessDataResult(string message) : base(default, true, message)
        {
            
        }
        // Sadece true ifadesini göndermek istediğinde
        public SuccessDataResult() : base(default, true)
        {
            
        }
    }
}
