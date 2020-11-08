using Microsoft.EntityFrameworkCore;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Helpers
{
    public class ProductLabelHelper
    {
        ICrudService<ProductLabelDto, int> m_productLabelService;

        public ProductLabelHelper
        (
            ICrudService<ProductLabelDto, int> productLabelService
        )
        {
            m_productLabelService = productLabelService;
        }


        public async Task AddLabelsAsync(int productId, List<LabelDto> labels)
        {
            foreach (var item in labels)
            {
                var productLabel = new ProductLabelDto
                {
                    ProductId = productId,
                    LabelId = item.Id
                };

                await m_productLabelService.AddAsync(productLabel);
            }
        }

        public async Task DeleteLabelsAsync(int productId)
        {
            var productLabels = await m_productLabelService
                .Where(item => item.ProductId == productId)
                .ToListAsync();

            foreach (var item in productLabels)
            {
                await m_productLabelService.DeleteAsync(item);
            }
        }
    }
}
