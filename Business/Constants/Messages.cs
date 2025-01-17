using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    // class static ise newlenmez
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        internal static string MaintenanceTime = "Bakım zamanı!";
        internal static string ProductListed = "Ürünler listelendi!";
        internal static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün bulunabilir.";
        internal static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        internal static string CategoryLimitExceded = "Kategori limiti aşıldı.";
    }
}
