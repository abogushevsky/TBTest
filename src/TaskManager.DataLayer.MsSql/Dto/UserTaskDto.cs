using System;
using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.DataLayer.MsSql.Dto
{
    public class UserTaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime DueDate { get; set; }

        public string UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public long ModifiedTimestamp { get; set; }
    }

    public class UserTaskConverter : IEntityDtoConverter<UserTask, UserTaskDto>
    {
        /// <summary>
        /// Коневертирует сущность в DTO
        /// </summary>
        public UserTaskDto Convert(UserTask entity)
        {
            Contract.Requires(entity.User != null);

            return new UserTaskDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                Details = entity.Details,
                DueDate = entity.DueDate,
                UserId = entity.User.Id,
                UserFirstName = entity.User.FirstName,
                UserLastName = entity.User.LastName
            };
        }

        /// <summary>
        /// Конвертирует DTO в сущность
        /// </summary>
        public UserTask Convert(UserTaskDto dto)
        {
            return new UserTask()
            {
                Id = dto.Id,
                Title = dto.Title,
                Details = dto.Details,
                DueDate = dto.DueDate,
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