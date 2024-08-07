﻿namespace MISA.Cukcuk.API.Model
{
    public class Employee
    {
        //public Guid EmpoyeeId { get; set; }
        //public string EmployeeCode { get; set; }
        //public string FullName { get; set; }
        //public int Gender { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        //public string Address { get; set; }
        //public string IdentityNumber { get; set; }
        //public DateTime IdentityDate { get; set; }
        //public string IdentityPlace { get; set; }
        //public Guid DepartmentId { get; set; }
        //public Guid PositionId { get; set; }
        //public string? LandlineNumber { get; set; }
        //public string? BankAccount { get; set; }
        //public string? BankName { get; set; }
        //public string? Branch {  get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? CreatedBy { get; set;}
        //public DateTime? ModifiedDate { get; set; }
        //public string? ModifiedBy { get; set;}
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        public string Email { get; set; }
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
