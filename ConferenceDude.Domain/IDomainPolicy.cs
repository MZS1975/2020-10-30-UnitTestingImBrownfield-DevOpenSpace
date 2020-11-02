namespace ConferenceDude.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IDomainPolicy<in TEntity, out TDomain> where TDomain : Enum
    {
        TDomain ViolationCode { get; }

        bool Check(IEnumerable<TEntity> entities, TEntity entityToValidate);
    }
}
