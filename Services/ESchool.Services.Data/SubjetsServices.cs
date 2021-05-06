namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Subject;

    public class SubjetsServices : ISubjectsService
    {
        private readonly IDeletableEntityRepository<Subject> subjectsRepository;
        private readonly IDeletableEntityRepository<ClassInSchool> classesRepository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;
        private readonly IDeletableEntityRepository<Lesson> lessonRepository;
        private readonly IClassesService classesService;

        public SubjetsServices(
            IDeletableEntityRepository<Subject> subjectsRepository,
            IDeletableEntityRepository<ClassInSchool> classesRepository,
            IDeletableEntityRepository<Quiz> quizRepository,
            IDeletableEntityRepository<Lesson> lessonRepository,
            IClassesService classesService)
        {
            this.subjectsRepository = subjectsRepository;
            this.classesRepository = classesRepository;
            this.quizRepository = quizRepository;
            this.lessonRepository = lessonRepository;
            this.classesService = classesService;
        }

        // Create Subject
        public async Task CreateAsync(CreateSubjectInputModel input, int classInSchoolId, string teacherId)
        {
            var subject = new Subject
            {
                Name = input.Name,
                ClassId = classInSchoolId,
                TeacherId = teacherId,
            };

            await this.subjectsRepository.AddAsync(subject);
            await this.subjectsRepository.SaveChangesAsync();
        }

        // Get all subjects of teacher
        public IEnumerable<SubjectAtListViewModel> GetAllSubjectOfTeacher<T>(string teacherId)
        {
            var subjects = this.subjectsRepository
                .AllAsNoTracking()
                .Where(x => x.TeacherId == teacherId)
                .OrderByDescending(x => x.Name)
                .To<SubjectAtListViewModel>()
                .ToList();

            return subjects;
        }

        // Get all subject of student
        public IEnumerable<SubjectAtListViewModel> GetAllSubjectsOfStudent<T>(ApplicationUser student)
        {
            var studentClass = this.classesService.GetClassOfStudent<ClassInSchool>(student);

            if (studentClass != null)
            {
                var subjects = this.subjectsRepository
                .AllAsNoTracking()
                .Where(x => x.ClassId == studentClass.Id)
                .OrderByDescending(x => x.Name)
                .To<SubjectAtListViewModel>()
                .ToList();

                return subjects;
            }

            return new List<SubjectAtListViewModel>();
        }

        // Get subject by id
        public T GetById<T>(int id)
        {
            var subject = this.subjectsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return subject;
        }

        public int GetIdByQuizId(string quizId)
        {
            var quiz = this.quizRepository
                .AllAsNoTracking()
                .Where(x => x.Id == quizId)
                .FirstOrDefault();

            var lesson = this.lessonRepository
                .AllAsNoTracking()
                .Where(x => x.Id == quiz.LessonId)
                .FirstOrDefault();

            var subject = this.subjectsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == lesson.SubjectId)
                .FirstOrDefault();

            return subject.Id;
        }
    }
}
