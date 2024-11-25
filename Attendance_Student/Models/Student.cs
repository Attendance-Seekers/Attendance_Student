using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace Student.Models
{
    public class Student : IdentityUser
    {
        public string Status { get; set; }
        public int ClassId { get; set; }
        public int ParentId { get; set; }
        public int AttendanceId { get; set; }
    }
}