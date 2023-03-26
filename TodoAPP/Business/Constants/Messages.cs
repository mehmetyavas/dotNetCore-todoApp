using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi!";
        public static string ProductNameInvalid= "Ürün Ismi Geçersi<!";
        public static string MaintenanceTime="Sistem Bakımda!";
        public static string ProductsListed= "Urunler Listelendi!";
        public static string ProductCountOfCategoryError="Bir Kategoride En Fazla 10 Ürün Olabilir";

        public static string AuthorizationDenied = "auth denied!";
        public static string UserRegistered="User Registered!";
        internal static string UserNotFound;
        internal static string PasswordError;
        internal static string SuccessfulLogin;
        internal static string AccessTokenCreated;
        internal static string UserAlreadyExists = "User Already Exist";
        internal static string ProductDeleted;
    }
}
