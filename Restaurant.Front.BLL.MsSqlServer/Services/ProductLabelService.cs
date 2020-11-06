using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.BLL.MsSqlServer.Services
{
    public class ProductLabelService : CrudService<ProductLabel, ProductLabelDto, int>
    {
        public ProductLabelService(ICrudRepository<ProductLabel, int> repository) : base(repository)
        {
        }
    }
}
