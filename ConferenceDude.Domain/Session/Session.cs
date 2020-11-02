namespace ConferenceDude.Domain.Session
{
    using Exceptions;

    public class Session
    {
        public SessionIdentity Id { get; }

        public string Abstract { get; private set; }

        public string Title { get; private set; }

        public bool IsNew => Id == SessionIdentity.NullIdentity;

        public Session(string title, string @abstract) : this(SessionIdentity.NullIdentity, title, @abstract)
        {
        }

        public Session(SessionIdentity id, string title, string @abstract)
        {
            Id = id;

            ChangeTitle(title);
            ChangeAbstract(@abstract);
        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ModelException($"{nameof(Title)} cannot be null or whitespace.", nameof(Title));
            }

            Title = title;
        }

        public void ChangeAbstract(string @abstract)
        {
            if (string.IsNullOrWhiteSpace(@abstract))
            {
                throw new ModelException($"{nameof(Abstract)} cannot be null or whitespace.", nameof(Abstract));
            }

            Abstract = @abstract;
        }
    }
}
