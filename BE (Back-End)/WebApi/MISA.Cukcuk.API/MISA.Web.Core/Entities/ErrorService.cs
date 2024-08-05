using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Entities
{
    public class ErrorService
    {
        public string devMsg { get; set; }
        public string userMsg { get; set; }
        public object data { get; set; }
    }
}
