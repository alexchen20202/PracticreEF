using System;
using System.ComponentModel.DataAnnotations;

namespace EfGetStart2.Models.SchoolViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate{get;set;}
        public int StudentCount{get;set;}
    }
}