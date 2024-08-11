using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.MISAAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty:Attribute
    {
    }

    public class PropName:Attribute 
    {
        public string Name;
        public PropName(string name) {
            Name = name;
        }

    }
}
