using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.DataLayer.MsSql.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public long ModifiedTimestamp { get; set; }
    }

    public class CategoryConverter : IEntityDtoConverter<Category, CategoryDto>
    {
        /// <summary>
        /// Коневертирует сущность в DTO
        /// </summary>
        public CategoryDto Convert(Category entity)
        {
            Contract.Requires(entity.User != null);

            return new CategoryDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                UserId = entity.User.Id,
                UserFirstName = entity.User.FirstName,
                UserLastName = entity.User.LastName
            };
        }

        /// <summary>
        /// Конвертирует DTO в сущность
        /// </summary>
        public Category Convert(CategoryDto dto)
        {
            return new Category()
            {
                Id = dto.Id,
                Name = dto.Name,
                User = new UserInfo()
                {
                    Id = dto.UserId,
                    FirstName = dto.UserFirstName,
                    LastName = dto.UserLastName
                }
            };
        }
    }
}