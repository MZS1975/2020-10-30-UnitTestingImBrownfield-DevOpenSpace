using ConferenceDude.Domain.Sessions;
using ConferenceDude.Server.Controllers;
using ConferenceDude.Server.Database;

namespace ConferenceDude.Server
{
    internal static class SessionMapper
    {
        public static Session ToSession(this SessionEntity entity)
        {
            return new Session
            {
                Id = entity.Id,
                Title = entity.Title,
                Abstract = entity.Abstract
            };
        }

        public static SessionEntity ToSessionEntity(this Session session)
        {
            return new SessionEntity
            {
                Id = session.Id,
                Title = session.Title,
                Abstract = session.Abstract
            };
        }

        public static Session ToSession(this SessionDto dto)
        {
            return new Session
            {
                Id = dto.Id,
                Title = dto.Title,
                Abstract = dto.Abstract
            };
        }

        public static SessionDto ToSessionDto(this Session session)
        {
            return new SessionDto
            {
                Id = session.Id,
                Title = session.Title,
                Abstract = session.Abstract
            };
        }
    }
}