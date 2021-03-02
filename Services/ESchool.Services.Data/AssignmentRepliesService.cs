namespace ESchool.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.AssignmentReply;
    using System.Linq;
    using ESchool.Services.Mapping;
    using ESchool.Common;

    public class AssignmentRepliesService : IAssignmentRepliesService
    {
        private readonly IDeletableEntityRepository<AssignmentReply> assignmentRepliesRepository;
        private readonly IDeletableEntityRepository<Material> materialRepository;

        public AssignmentRepliesService(
            IDeletableEntityRepository<AssignmentReply> assignmentRepliesRepository,
            IDeletableEntityRepository<Material> materialRepository)
        {
            this.assignmentRepliesRepository = assignmentRepliesRepository;
            this.materialRepository = materialRepository;
        }

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

        public T GetById<T>(string assignmentReplyId)
        {
            var assignmentReply = this.assignmentRepliesRepository.AllAsNoTracking()
                .Where(x => x.Id == assignmentReplyId)
                .To<T>()
                .FirstOrDefault();

            return assignmentReply;
        }

        public int GetCountRepliesOfStudent(string studentId)
        {
            return this.assignmentRepliesRepository
                .AllAsNoTracking()
                .Where(x => x.StudentId == studentId)
                .Count();
        }

        public async Task SendAsync(SendAssignmentReplyInputModel input, string materialPath)
        {
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


                    //if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    //{
                    //    throw new Exception($"Invalid image extension {extension}");
                    //}

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
