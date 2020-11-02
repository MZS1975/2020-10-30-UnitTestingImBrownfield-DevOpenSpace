namespace ConferenceDude.UI.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
                    AllowMultiple = false)]
    public class RegExValidationAttribute : ValidationAttribute
    {
        public string Pattern { get; set; }
        public RegexOptions Options { get; set; }

        public RegExValidationAttribute(string pattern) : this(pattern, RegexOptions.None) { }
        public RegExValidationAttribute(string pattern, RegexOptions options)
        {
            Pattern = pattern;
            Options = options;
        }

        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value.ToString(), Pattern, Options);
        }
    }
}
