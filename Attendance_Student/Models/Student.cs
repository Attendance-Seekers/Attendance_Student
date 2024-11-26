using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class Student : IdentityUser
    {
        public string Status { get; set; }

        [ForeignKey("_class")]
        public int Class_Id { get; set; }
        public virtual Class _class { get; set; }
        //public int ParentId { get; set; }
        //public int AttendanceId { get; set; }
    }
}