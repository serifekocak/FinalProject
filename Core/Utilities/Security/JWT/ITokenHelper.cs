using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    // Jwt'den başka bir yöntem kullanılmak istenirse
    public interface ITokenHelper
    {
        // İlgili kullanıcı için kullanıcı claimlerini içeren bir token üret
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
