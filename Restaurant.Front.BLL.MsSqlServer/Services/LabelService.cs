using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.BLL.MsSqlServer.Services
{
    public class LabelService : CrudService<Label, LabelDto, int>
    {
        public LabelService(ICrudRepository<Label, int> repository) : base(repository)
        {
        }
    }
}
