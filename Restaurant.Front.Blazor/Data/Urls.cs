using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Blazor.Data
{
    public class Urls
    {
        public const string FrontApi = "https://localhost:44390/api";
        public const string FrontApiProduct = FrontApi + "/product";
        public const string FrontApiOrderInfo = FrontApi + "/OrderInfo";
        public const string FrontApiCurrentCart = FrontApiOrderInfo + "/CurrentCart";
        public const string FrontApiOrderPosition = FrontApi + "/OrderPosition";

        public const string BackApi = "https://localhost:44306/api";
        public const string BackApiCart = BackApi + "/Cart";
    }
}
