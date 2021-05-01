namespace ESchool.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Lesson;

    public class LessonsService : ILessonsService
    {
        private readonly string[] allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".png", ".jpg", ".jpeg" };

        private readonly IDeletableEntityRepository<Lesson> lessonRepository;
        private readonly IDeletableEntityRepository<Material> materialRepository;
        private readonly IDeletableEntityRepository<Chat> chatRepository;

        public LessonsService(
            IDeletableEntityRepository<Lesson> lessonRepository,
            IDeletableEntityRepository<Material> materialRepository,
            IDeletableEntityRepository<Chat> chatRepository)
        {
            this.lessonRepository = lessonRepository;
            this.materialRepository = materialRepository;
            this.chatRepository = chatRepository;
        }

        public async Task CreateAsync(CreateLessonInputModel input, string materialPath)
        {
            if (input.Description == null || input.Description.Length <= 15)
            {
                throw new Exception("");
            }

            var lesson = new Lesson
            {
                Name = input.Name,
                Description = input.Description,
                SubjectId = input.SubjectId,
                UserId = input.TeacherId,
            };

            await this.lessonRepository.AddAsync(lesson);
            await this.lessonRepository.SaveChangesAsync();

            // Chat
            var chat = new Chat
            {
                Name = lesson.Name,
            };

            await this.chatRepository.AddAsync(chat);
            await this.chatRepository.SaveChangesAsync();

            lesson.ChatId = chat.Id;
            await this.chatRepository.SaveChangesAsync();

            if (input.Materials != null)
            {
                Directory.CreateDirectory($"{materialPath}/materials/");
                foreach (var material in input.Materials)
                {
                    var extension = Path.GetExtension(material.FileName);


                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Невалиден файлов формат: {extension}");
                    }

                    var dbMaterial = new Material
                    {
                        Name = material.FileName,
                        UserId = input.TeacherId,
                        LessonId = lesson.Id,
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

        public async Task UpdateAsync(EditLessonInputModel input, int id)
        {
            if (input.Description == null || input.Description.Length <= 15)
            {
                throw new Exception("");
            }

            var lesson = this.lessonRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            lesson.Name = input.Name;
            lesson.Description = input.Description;

            await this.lessonRepository.SaveChangesAsync();
        }


        // Return lesson by its Id
        public T GetById<T>(int id)
        {
            var lesson = this.lessonRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return lesson;
        }
    }
}
