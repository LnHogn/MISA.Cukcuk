using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Interfaces.Services;
using MISA.Web.Core.MISAAttribute;
using MISA.Web.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace MISA.Web.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public int InsertService(MISAEntity entity)
        {
            //validate
            ValidateData(entity);
            ValidateEmployee(entity);
            var res = _baseRepository.Insert(entity);
            return res;
        }

        public int UpdateService(MISAEntity entity, Guid entityId)
        {
            var res = _baseRepository.Update(entity, entityId);
            return  res;
        }

        private void ValidateData(MISAEntity entity)
        {
            var props = entity.GetType().GetProperties();
            var propNotEmp = entity.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(NotEmpty))); 
            foreach ( var prop in propNotEmp)
            {
                var propVal = prop.GetValue(entity);
                var propName = prop.Name;
                var nameDisplay = string.Empty;
                var propNames = prop.GetCustomAttributes(typeof(PropName),true);
                if(propNames.Length > 0)
                {
                    nameDisplay = (propNames[0] as PropName).Name;
                }
                if(propVal == null || string.IsNullOrEmpty(propVal.ToString()))
                {
                    nameDisplay = (nameDisplay == string.Empty ? propName : nameDisplay);
                    throw new MISAValidateException(string.Format(Core.Resources.ResourceVN.InfoNotEmpty,nameDisplay));
                }
            }
        }

        protected virtual void ValidateEmployee(MISAEntity entity)
        {

        }
    }
}
