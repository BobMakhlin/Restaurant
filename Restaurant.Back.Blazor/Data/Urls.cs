using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Data
{
    public class Urls
    {
        public const string BackApi = "https://localhost:44306/api";
        public const string BackApiCategory = BackApi + "/category";
        public const string BackApiIngredient = BackApi + "/ingredient";
        public const string BackApiOrder = BackApi + "/order";
        public const string BackApiStatus = BackApi + "/status";
        public const string BackApiProduct = BackApi + "/product";
    }
}