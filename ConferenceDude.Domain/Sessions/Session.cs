using ConferenceDude.Domain.Shared;

namespace ConferenceDude.Domain.Sessions
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }

        public ValidationResult Validate()
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrEmpty(Title))
                validationResult.AddError(nameof(Title), "Das Feld ist ein Pflichtfeld");

            if (string.IsNullOrEmpty(Abstract))
                validationResult.AddError(nameof(Abstract), "Das Feld ist ein Pflichtfeld");

            return validationResult;
        }

        public bool IsNew()
        {
            return Id == 0;
        }
    }
}