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
        [PrimaryKey]
        public Guid PositionId { get; set; }
        [NotEmpty]
        [PropName("Mã vị trí")]
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        [DateAddorMod]
        public DateTime? CreatedDate { get; set; }
        [AddorModBy]
        public string? CreatedBy { get; set; }
        [DateAddorMod]
        public DateTime? ModifiedDate { get; set; }
        [AddorModBy]
        public string? ModifiedBy { get; set;}
    }
}
