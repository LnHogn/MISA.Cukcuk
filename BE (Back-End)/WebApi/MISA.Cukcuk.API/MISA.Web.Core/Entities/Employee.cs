using MISA.Web.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        [NotEmpty]
        [PropName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        [NotEmpty]
        [PropName("Họ tên nhân viên")]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        [NotEmpty]
        [PropName("Số cccd")]
        public string IdentityNumber { get; set; }
        public DateTime IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        [NotEmpty]
        public string Email { get; set; }
        [NotEmpty]
        [PropName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? LandlineNumber { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public Guid PositionId { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
