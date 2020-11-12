using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Helpers.Synchronizers
{
    public class IngredientSynchronizer
    {
        #region Fields
        ICrudService<IngredientDto, int> m_ingredientsService;
        #endregion

        public IngredientSynchronizer
        (
            ICrudService<IngredientDto, int> ingredientsService
        )
        {
            m_ingredientsService = ingredientsService;
        }


        public async Task SynchronizeAsync(List<IngredientDto> backIngredients)
        {
            foreach (var backIngredient in backIngredients)
            {
                var frontIngredient = await m_ingredientsService.GetAsync(backIngredient.Id);


                if (frontIngredient == null)
                {
                    await AddIngredientAsync(backIngredient);
                }
                else
                {
                    await UpdateIngredientAsync(frontIngredient, backIngredient);
                }
            }
        }

        private async Task AddIngredientAsync(IngredientDto backIngredient)
        {
            await m_ingredientsService.AddAsync(backIngredient);
        }
        private async Task UpdateIngredientAsync(IngredientDto frontIngredient, IngredientDto backIngredient)
        {
            frontIngredient.Title = backIngredient.Title;
            frontIngredient.Price = backIngredient.Price;
            frontIngredient.IsEnabled = backIngredient.IsEnabled;

            await m_ingredientsService.UpdateAsync(frontIngredient);
        }
    }
}
