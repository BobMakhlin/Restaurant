using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Helpers.Synchronizers
{
    public class CategorySynchronizer
    {
        #region Fields
        ICrudService<CategoryDto, int> m_categoriesService;
        #endregion

        public CategorySynchronizer
        (
            ICrudService<CategoryDto, int> categoriesService
        )
        {
            m_categoriesService = categoriesService;
        }


        public async Task SynchronizeAsync(List<CategoryDto> backCategories)
        {
            foreach (var backCategory in backCategories)
            {
                var frontCategory = await m_categoriesService.GetAsync(backCategory.Id);

                
                if (frontCategory == null)
                {
                    await AddCategoryAsync(backCategory);
                }
                else
                {
                    await UpdateCategoryAsync(backCategory);
                }
            }
        }

        private async Task AddCategoryAsync(CategoryDto backCategory)
        {
            await m_categoriesService.AddAsync(backCategory);
        }

        private async Task UpdateCategoryAsync(CategoryDto backCategory)
        {
            await m_categoriesService.UpdateAsync(backCategory);
        }
    }
}
