using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        // Data, false ve message göndermek istediğinde
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        // Sadece data ve false göndermek istediğinde
        public ErrorDataResult(T data) : base(data, false)
        {

        }
        // Sadece message göndermek istediğinde
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }
        // Sadece false ifadesini göndermek istediğinde
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
