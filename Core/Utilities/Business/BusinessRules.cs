using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    // İş motoru
    public class BusinessRules
    {
        // Tool olduğu için static yap.  
        // params ile metota istediğimiz kadar IResult verebiliriz. params yerine parametre olarak Liste de istenilebilirdi.
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                // parametre ile gönderilen iş kurallarından başarısız olanları business'a döndür
                if (!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}
