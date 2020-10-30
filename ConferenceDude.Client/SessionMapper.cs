using ConferenceDude.Domain.Sessions;

namespace ConferenceDude.Client
{
    internal static class SessionMapper
    {
        public static Session ToSession(this SessionModel model)
        {
            return new Session
            {
                Id = model.Id,
                Title = model.Title, 
                Abstract = model.Abstract
            };
        }

        public static SessionModel ToSessionModel(this Session session)
        {
            return new SessionModel
            {
                Id = session.Id,
                Title = session.Title,
                Abstract = session.Abstract
            };
        }
    }
}