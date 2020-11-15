using Microsoft.JSInterop;
using Restaurant.Front.Blazor.Data;
using Restaurant.Front.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Restaurant.Front.Blazor.Helpers
{
    public class CartHelper
    {
        #region Fields
        IJSRuntime m_jsRuntime;
        HttpClient m_httpClient;
        #endregion

        #region Constructor
        public CartHelper(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            m_jsRuntime = jsRuntime;
            m_httpClient = httpClient;
        }
        #endregion


        #region Methods
        public async Task AddProductToCart(int productId)
        {
            await m_jsRuntime.InvokeVoidAsync("cart.totalCount.increase");


            var userId = await m_jsRuntime.InvokeAsync<string>("localStorage.getItem", Keys.UserIdLocalStorageKey);

            if (userId == null)
            {
                await AddProductToGuestCart(productId);
            }
            else
            {
                await AddProductToUserCart(userId, productId);
            }
        }


        private async Task AddProductToGuestCart(int productId)
        {
            var userGuid = Guid.NewGuid();
            await m_jsRuntime.InvokeVoidAsync("localStorage.setItem", Keys.UserIdLocalStorageKey, userGuid.ToString());

            await CreateCart(userGuid, productId);
        }

        private async Task AddProductToUserCart(string userId, int productId)
        {
            var url = $"{Urls.FrontApiCurrentCart}/{userId}";
            var response = await m_httpClient.GetAsync(url);


            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var userGuid = Guid.Parse(userId);
                await CreateCart(userGuid, productId);
            }
            else
            {
                var currentOrder = await response.Content.ReadFromJsonAsync<OrderPoco>();
                await AddProductToExistingCart(currentOrder, productId);
            }
        }


        private async Task CreateCart(Guid userGuid, int productId)
        {
            var order = new OrderPoco
            {
                UserId = userGuid,
                OrderPosition = new List<OrderPositionPoco>
            {
                new OrderPositionPoco { ProductId = productId, Amount = 1 }
            }
            };

            await m_httpClient.PostAsJsonAsync(Urls.FrontApiOrderInfo, order);
        }

        private async Task AddProductToExistingCart(OrderPoco order, int productId)
        {
            var existingPosition = order.OrderPosition
                .FirstOrDefault(item => item.ProductId == productId);


            if (existingPosition == null)
            {
                var position = new OrderPositionPoco
                {
                    OrderInfoId = order.Id,
                    ProductId = productId,
                    Amount = 1
                };

                await m_httpClient.PostAsJsonAsync(Urls.FrontApiOrderPosition, position);
            }
            else
            {
                existingPosition.Amount++;

                var url = $"{Urls.FrontApiOrderPosition}/{existingPosition.Id}";
                await m_httpClient.PutAsJsonAsync(url, existingPosition);
            }
        }
        #endregion
    }
}
