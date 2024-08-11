﻿using MISA.Web.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Entities
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        [NotEmpty]
        [PropName("Mã phòng ban")]
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
