using System.Collections.Generic;
using System.Linq;

namespace ConferenceDude.Domain.Shared
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Messages = new List<(string FieldName, string ErrorMessage)>();
        }

        public bool Success => !Messages.Any();
        public IList<(string FieldName, string ErrorMessage)> Messages { get; }

        public void AddError(string fieldName, string errorMessage)
        {
            Messages.Add((fieldName, errorMessage));
        }
    }
}