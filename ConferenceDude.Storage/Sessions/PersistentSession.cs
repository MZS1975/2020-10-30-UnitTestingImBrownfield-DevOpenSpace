namespace ConferenceDude.Storage.Sessions
{
    public class PersistentSession
    {
        public PersistentSession(string title, string @abstract)
        {
            Title = title;
            Abstract = @abstract;
        }

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
    }
}