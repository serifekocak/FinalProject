using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IDataResult<T> : IResult
    {
        // success ve message'a ek olarak data da içerir.
        T Data { get; }
    }
}
