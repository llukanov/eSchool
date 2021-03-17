namespace ESchool.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.AssignmentReply;

    public class AssignmentRepliesService : IAssignmentRepliesService
    {
        private readonly string[] allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".png", ".jpg", ".jpeg" };

        private readonly IDeletableEntityRepository<AssignmentReply> assignmentRepliesRepository;
        private readonly IDeletableEntityRepository<Material> materialRepository;

        public AssignmentRepliesService(
            IDeletableEntityRepository<AssignmentReply> assignmentRepliesRepository,
            IDeletableEntityRepository<Material> materialRepository)
        {
            this.assignmentRepliesRepository = assignmentRepliesRepository;
            this.materialRepository = materialRepository;
        }

        // Get all replies of some assignment by AssignmentId
        public IEnumerable<AssignmentReplyAtListViewModel> GetAllRepliesOfAssignment<T>(string assignmentId)
        {
            var replies = this.assignmentRepliesRepository
                .AllAsNoTracking()
                .Where(x => x.AssignmentId == assignmentId)
                .OrderBy(x => x.Student.FirstName)
                .To<AssignmentReplyAtListViewModel>()
                .ToList();

            return replies;
        }

        // Get all replies of some student by StudentId
        public IEnumerable<AssignmentReplyAtListViewModel> GetAllRepliesOfStudent<T>(string studentId, int page, int itemsPerPage = 20)
        {
            var replies = this.assignmentRepliesRepository
                .AllAsNoTracking()
                .Where(x => x.StudentId == studentId)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<AssignmentReplyAtListViewModel>()
                .ToList();

            return replies;
        }

        // Get all replies of some assignment by replyId
        public T GetById<T>(string assignmentReplyId)
        {
            var assignmentReply = this.assignmentRepliesRepository.AllAsNoTracking()
                .Where(x => x.Id == assignmentReplyId)
                .To<T>()
                .FirstOrDefault();

            return assignmentReply;
        }

        // Get count of all replies of student
        public int GetCountRepliesOfStudent(string studentId)
        {
            return this.assignmentRepliesRepository
                .AllAsNoTracking()
                .Where(x => x.StudentId == studentId)
                .Count();
        }

        // Send reply and processing uploaded files
        public async Task SendAsync(SendAssignmentReplyInputModel input, string materialPath)
        {
            if (input.Description == null || input.Description.Length <= 100)
            {
                throw new Exception("");
            }

            var assignmentReply = new AssignmentReply
            {
                Text = input.StudentReplyText,
                AssignmentId = input.AssignmentId,
                StudentId = input.StudentId,
                GradeId = GlobalConstants.DefaultGradeId,
            };

            await this.assignmentRepliesRepository.AddAsync(assignmentReply);
            await this.assignmentRepliesRepository.SaveChangesAsync();

            if (input.StudentReplyFiles != null)
            {
                Directory.CreateDirectory($"{materialPath}/materials/");
                foreach (var files in input.StudentReplyFiles)
                {
                    var extension = Path.GetExtension(files.FileName);


                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Невалиден файлов формат: {extension}");
                    }

                    var dbMaterial = new Material
                    {
                        Name = files.FileName,
                        UserId = input.StudentId,
                        AssignmentReplyId = assignmentReply.Id,
                        Extension = extension,
                    };

                    await this.materialRepository.AddAsync(dbMaterial);

                    var physicalPath = $"{materialPath}/materials/{dbMaterial.Id}{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await files.CopyToAsync(fileStream);
                }

                await this.materialRepository.SaveChangesAsync();
            }
        }

        // Update reply - reviewing and setting grade of student reply
        public async Task UpdateAsync(ReturnAssignmentReplyInputModel input, string assignmentReplyId)
        {
            var assignmentReply = this.assignmentRepliesRepository
                .All()
                .FirstOrDefault(x => x.Id == assignmentReplyId);

            assignmentReply.TeacherReview = input.TeacherReview;
            assignmentReply.GradeId = input.GradeId;

            await this.assignmentRepliesRepository.SaveChangesAsync();
        }
    }
}
