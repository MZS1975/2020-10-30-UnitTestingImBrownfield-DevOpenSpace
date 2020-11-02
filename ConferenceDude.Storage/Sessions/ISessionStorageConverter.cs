namespace ConferenceDude.Storage.Sessions
{
    using Domain.Session;

    public interface ISessionStorageConverter
    {
        Session FromDatabaseModel(PersistentSession persistentSession);
        PersistentSession ToDatabaseModel(Session session);
    }
}
