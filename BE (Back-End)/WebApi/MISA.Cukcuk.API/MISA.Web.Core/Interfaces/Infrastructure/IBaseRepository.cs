﻿using MISA.Web.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<MISAEntity>
    {
        IEnumerable<MISAEntity> GetAll();

        int Insert(MISAEntity entity);
        int Update(MISAEntity entity, Guid entityId);
        int Delete(Guid entityId);
    }
}
