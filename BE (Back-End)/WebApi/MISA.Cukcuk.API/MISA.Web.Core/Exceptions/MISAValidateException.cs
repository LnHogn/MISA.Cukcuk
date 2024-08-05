using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Exceptions
{
    public class MISAValidateException : Exception
    {
        string? MsgErrValidate = null;
        public MISAValidateException(string msg) 
        {
            this.MsgErrValidate = msg;
        }

        public override string Message
        {
            get
            {
                return MsgErrValidate;
            }
        }

    }
}
