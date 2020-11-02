namespace ConferenceDude.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using Domain.Session;
    using JetBrains.Annotations;
    using Validation;

    public class SessionVewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>>
            _validationErrors = new Dictionary<string, ICollection<string>>();

        private string _title;
        private string _abstract;

        public SessionVewModel()
        {
            Id = 0;
            _title = string.Empty;
            _abstract = string.Empty;
            ValidateObject();
        }

        public SessionVewModel(Session session)
        {
            Id = session.Id;
            _title = session.Title;
            _abstract = session.Abstract;
            ValidateObject();
        }

        public int Id { get; }

        [Required(ErrorMessage = "Please provide a title!", AllowEmptyStrings = false), RegExValidation(@"^(?!\s+$).+$", RegexOptions.Multiline | RegexOptions.ECMAScript, ErrorMessage = "Title must not consist of whitespace only!")]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;

                ValidateModelProperty(value, nameof(Title));
                _title = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Please provide an abstract!", AllowEmptyStrings = false), RegExValidation(@"^(?!\s+$).+$", RegexOptions.Multiline | RegexOptions.ECMAScript, ErrorMessage = "Abstract must not consist of whitespace only!")]
        public string Abstract
        {
            get => _abstract;
            set
            {
                if (value == _abstract) return;

                ValidateModelProperty(value, nameof(Abstract));
                _abstract = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Session ToSession()
        {
            return new Session(new SessionIdentity(Id), Title, Abstract);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _validationErrors.ContainsKey(propertyName) ? _validationErrors[propertyName] : new List<string>();
        }

        [Browsable(false)]
        public bool HasErrors => _validationErrors.Any(e => e.Value.Any());

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        protected void ValidateModelProperty(object value, string propertyName)
        {

            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);

            PropertyInfo? propertyInfo = GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                IList<string> validationErrors = (from validationAttribute in propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>()
                                                  where !validationAttribute.IsValid(value)
                                                  select validationAttribute.FormatErrorMessage(string.Empty)).ToList();

                _validationErrors.Add(propertyName, validationErrors);
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ValidateObject()
        {
            ValidateModelProperty(_title, nameof(Title));
            ValidateModelProperty(_abstract, nameof(Abstract));
        }
    }
}