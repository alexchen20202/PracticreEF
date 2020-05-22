using System;
using System.ComponentModel.DataAnnotations;

namespace EfGetStart2.Models.SchoolViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate{get;set;}
        public int StudentCount{get;set;}
    }
}