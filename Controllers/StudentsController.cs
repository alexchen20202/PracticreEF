using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfGetStart2.DAL;
using EfGetStart2.Models;
using EfGetStart2.Services;

namespace EfGetStart2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortString = "", string searchString = "", int page = 1)
        {
            
            ViewBag.SearchString = searchString;
            ViewBag.SortString = sortString;
            ViewBag.SortFirstName = (sortString == "firstName_desc") ? "firstName" : "firstName_desc";        
            ViewBag.SortLastName = (sortString == "lastName_desc") ? "lastname" : "lastName_desc";
            ViewBag.SortEnrollDate = (sortString == "enrollDate_desc") ? "enrollDate" : "enrollDate_desc";
            
            IQueryable<Student> students =  _context.Students.AsQueryable();

            if(!String.IsNullOrEmpty(searchString))
            {
                page = 1;
                students = students.Where(
                    s => s.FirstName.Contains(searchString)
                    || s.LastName.Contains(searchString)
                );
            }

            switch(sortString)
            {
                case "firstName_desc":
                students = students.OrderByDescending(s => s.FirstName);
                break;
                case "firstName":
                students = students.OrderBy(s => s.FirstName);
                break;
                case "lastName_desc":
                students = students.OrderByDescending(s => s.LastName);
                break;
                case "lastName":
                students = students.OrderBy(s => s.LastName);
                break;
                case "enrollDate_desc":
                students = students.OrderByDescending(s => s.EnrollmentDate);
                break;
                case "enrollDate":
                students = students.OrderBy(s => s.EnrollmentDate);
                break;
                default :
                students = students.OrderBy(s => s.FirstName);
                break;
            }
            
            int pageSize = 5;
            PagedList<Student> pageList = new PagedList<Student>();
            students = await pageList.Paging(students, page, pageSize);

            ViewBag.HasNextPage = pageList.HasNextPage;
            ViewBag.HasPrevPage = pageList.HasPrevPage;
            ViewBag.TotalPages = pageList.TotalPages;
            ViewBag.PageIndex = pageList.PageIndex;
            
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,EnrollmentDate")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
