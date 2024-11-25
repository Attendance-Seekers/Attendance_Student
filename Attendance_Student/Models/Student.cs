using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SchoolManagement.Models
{
    public class Student
    {
        public string Status { get; set; }
        public int ClassId { get; set; }
        public int ParentId { get; set; }
        public int AttendanceId { get; set; }
    }
}