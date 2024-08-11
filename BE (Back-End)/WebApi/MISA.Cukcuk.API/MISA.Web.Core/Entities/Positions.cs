using MISA.Web.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Entities
{
    public class Positions
    {
        public Guid PositionId { get; set; }
        [NotEmpty]
        [PropName("Mã vị trí")]
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set;}
    }
}
