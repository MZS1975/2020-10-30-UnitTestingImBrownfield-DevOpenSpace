namespace ConferenceDude.Domain
{
    using System;
    using System.Collections.Generic;

    public readonly struct PolicyCheckResult<TDomain> where TDomain : Enum
    {
        public IReadOnlyList<TDomain>? Violations { get; }

        public bool IsValid => (Violations?.Count ?? 0) == 0;

        public PolicyCheckResult(IEnumerable<TDomain> violations)
        {
            Violations = new List<TDomain>(violations);
        }

        public PolicyCheckResult<TDomain> AddViolation(TDomain violationCode)
        {
            var violations = new List<TDomain>(Violations ?? new List<TDomain>())
            {
                violationCode
            };
            return new PolicyCheckResult<TDomain>(violations);
        }
    }
}
