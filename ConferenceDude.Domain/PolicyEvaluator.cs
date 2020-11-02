namespace ConferenceDude.Domain
{
    using System;
    using System.Collections.Generic;

    public class PolicyEvaluator<TEntity, TDomain> where TDomain : Enum
    {
        private readonly ICollection<IDomainPolicy<TEntity, TDomain>> _policies = new List<IDomainPolicy<TEntity, TDomain>>();

        public void AddPolicy(IDomainPolicy<TEntity, TDomain> policy)
        {
            _policies.Add(policy);
        }

        public PolicyCheckResult<TDomain> Evaluate(IEnumerable<TEntity> entities, TEntity entityToValidate)
        {
            var result = new PolicyCheckResult<TDomain>();
            foreach (IDomainPolicy<TEntity, TDomain> domainPolicy in _policies)
            {
                if (!domainPolicy.Check(entities, entityToValidate))
                {
                    result = result.AddViolation(domainPolicy.ViolationCode);
                }
            }

            return result;
        }
    }
}
