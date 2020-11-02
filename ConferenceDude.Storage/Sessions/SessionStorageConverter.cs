namespace ConferenceDude.Storage.Sessions
{
    using Domain.Session;

    public class SessionStorageConverter : ISessionStorageConverter
    {
        public Session FromDatabaseModel(PersistentSession persistentSession)
        {
            return new Session(new SessionIdentity(persistentSession.Id ?? 0), persistentSession.Title, persistentSession.Abstract);
        }

        public PersistentSession ToDatabaseModel(Session session)
        {
            return new PersistentSession(session.Title, session.Abstract)
            {
                Id = (session.Id != 0 ? session.Id : (int?)null)
            };
        }
    }
}
