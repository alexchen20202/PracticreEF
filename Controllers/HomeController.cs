using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EfGetStart2.Models;
using Microsoft.EntityFrameworkCore;
using EfGetStart2.Models.SchoolViewModels;
using EfGetStart2.DAL;

namespace EfGetStart2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _context;

        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> About()
        {  
            IQueryable<IGrouping<DateTime, Student>> groupList = 
            _context.Students.GroupBy(s => s.EnrollmentDate);
            
            List<EnrollmentDateGroup> eGroupList = new List<EnrollmentDateGroup>();
            
            foreach(IGrouping<DateTime, Student> group in groupList)
            {
                EnrollmentDateGroup eGroup = new EnrollmentDateGroup();
                eGroup.EnrollmentDate = group.Key;
                eGroup.StudentCount = group.Count();
                eGroupList.Add(eGroup);
            } 
            
            return View(eGroupList);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
