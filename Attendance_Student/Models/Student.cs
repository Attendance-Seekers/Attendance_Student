using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.Models
{
    public class Student : IdentityUser
    {
        [MaxLength(100)]
        public string Student_fullname { get; set; }
        [Range(5, 17, ErrorMessage = "Age must be between 5 and 17 years.")]

        public int age { get; set; }
        public string status { get; set; }

        [ForeignKey("_class")]
        public int ClassId { get; set; }
        public virtual Class _class { get; set; }
        [ForeignKey("Parent")]
        [Required(ErrorMessage = "Parent ID is required.")]
        public string ParentId { get; set; }
        public virtual Parent Parent { get; set; }
        public virtual List<StudentAttendance> viewAttendances { get; set; } = new List<StudentAttendance>();
    }
}