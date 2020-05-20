using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EfGetStart2.Models
{
    public partial class Student
    {
        public int StudentId{get;set;}
        
        [Required]
        public string FirstName{get;set;}
        
        [Required]
        public string LastName{get;set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate{get;set;}

        public virtual ICollection<Enrollment> Enrollments{get;set;}   
    }
}