using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Services.Mapping;
using ESchool.Web.ViewModels.Grade;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data
{
    public class StudentGradesServices : IStudentGradesServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Subject> subjectRepository;
        private readonly IDeletableEntityRepository<StudentGrade> studentGradeRepository;

        public StudentGradesServices(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Subject> subjectRepository,
            IDeletableEntityRepository<StudentGrade> studentGradeRepository)
        {
            this.userManager = userManager;
            this.subjectRepository = subjectRepository;
            this.studentGradeRepository = studentGradeRepository;
        }

        public async Task AddGrade(int subjectId, string studentId, string teacherId, int gradeId)
        {
            var grade = new StudentGrade
            {
                Text = "",
                StudentId = studentId,
                TeachertId = teacherId,
                SubjectId = subjectId,
                GradeId = gradeId,
            };

            await this.studentGradeRepository.AddAsync(grade);
            await this.studentGradeRepository.SaveChangesAsync();
        }

        // Get all grades in some project
        public IEnumerable<StudentGradeAtListViewModel> GetAllInSubject<T>(int subjectId)
        {
            var subject = this.subjectRepository
                .AllAsNoTracking()
                .Where(x => x.Id == subjectId)
                .FirstOrDefault();

            var students = this.userManager.Users
                .Where(x => x.ClassInSchool.Subjects.Contains(subject))
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .To<StudentGradeAtListViewModel>()
                .ToList();

            return students;
        }

        // Get all grades in some project
        public IEnumerable<T> GetAllOfStudents<T>(string studentId)
        {
            var grades = this.studentGradeRepository
                .AllAsNoTracking()
                .Where(x => x.StudentId == studentId)
                .OrderBy(x => x.CreatedOn)
                .To<T>()
                .ToList();

            return grades;
        }
    }
}
