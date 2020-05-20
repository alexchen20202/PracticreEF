using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfGetStart2.Models
{
    public partial class Course
    {
        
        public int CourseID{get;set;}
        
        [Required]
        public string Title{get;set;}
        public int Credits{get;set;}

        public virtual ICollection<Enrollment> Enrollments{get;set;}
    }
}