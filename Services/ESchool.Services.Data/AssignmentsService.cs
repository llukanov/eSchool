using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Web.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ESchool.Services.Mapping;

namespace ESchool.Services.Data
{
    public class AssignmentsService : IAssignmentsService
    {
        private readonly IDeletableEntityRepository<Assignment> assignmentRepository;
        private readonly IDeletableEntityRepository<Material> materialRepository;

        public AssignmentsService(
            IDeletableEntityRepository<Assignment> assignmentRepository,
            IDeletableEntityRepository<Material> materialRepository)
        {
            this.assignmentRepository = assignmentRepository;
            this.materialRepository = materialRepository;
        }

        public async Task CreateAsync(CreateAssignmentInputModel input, string materialPath)
        {
            var assignment = new Assignment
            {
                Description = input.Description,
                LessonId = input.LessonId,
                TeacherId = input.TeacherId,
                Deadline = input.Deadline,
            };

            await this.assignmentRepository.AddAsync(assignment);
            await this.assignmentRepository.SaveChangesAsync();

            if (input.Materials != null)
            {
                Directory.CreateDirectory($"{materialPath}/materials/");
                foreach (var material in input.Materials)
                {
                    var extension = Path.GetExtension(material.FileName);


                    //if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    //{
                    //    throw new Exception($"Invalid image extension {extension}");
                    //}

                    var dbMaterial = new Material
                    {
                        Name = material.FileName,
                        UserId = input.TeacherId,
                        AssignmentId = assignment.Id,
                        Extension = extension,
                    };

                    await this.materialRepository.AddAsync(dbMaterial);

                    var physicalPath = $"{materialPath}/materials/{dbMaterial.Id}{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await material.CopyToAsync(fileStream);
                }

                await this.materialRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<CreateAssignmentInputModel> GetAll<T>(int page, int itemsPerPage = 20)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentAtListViewModel> GetAllAssignmensInLesson<T>(int lessonId)
        {
            var assigments = this.assignmentRepository
                .AllAsNoTracking()
                .Where(x => x.LessonId == lessonId)
                .OrderByDescending(x => x.CreatedOn)
                .To<AssignmentAtListViewModel>()
                .ToList();

            return assigments;
        }

        public IEnumerable<AssignmentAtListViewModel> GetAllInSubject<T>(int subjectId, int page, int itemsPerPage = 20)
        {
            var assignments = this.assignmentRepository
                .AllAsNoTracking()
                .Where(x => x.Lesson.SubjectId == subjectId)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<AssignmentAtListViewModel>()
                .ToList();

            return assignments;
        }

        public T GetById<T>(string assignmentId)
        {
            var assignment = this.assignmentRepository.AllAsNoTracking()
                .Where(x => x.Id == assignmentId)
                .To<T>()
                .FirstOrDefault();

            return assignment;
        }

        public int GetCountInSubject(int subjectId)
        {
            return this.assignmentRepository
                .AllAsNoTracking()
                .Where(x => x.Lesson.SubjectId == subjectId)
                .Count();
        }

        public async Task UpdateAsync(EditAssignmentInputModel input, string assignmentId)
        {
            var assignment = this.assignmentRepository
                .All()
                .FirstOrDefault(x => x.Id == assignmentId);

            assignment.Description = input.Description;
            assignment.Deadline = input.Deadline;

            await this.assignmentRepository.SaveChangesAsync();
        }
    }
}
