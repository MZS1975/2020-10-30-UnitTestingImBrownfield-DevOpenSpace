namespace ConferenceDude.Application.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenericRestClient<TEntity, in TEntityKey>
        where TEntity : struct 
        where TEntityKey : struct
    {
        public Task<IEnumerable<TEntity>> ListAsync();

        public Task<TEntity?> GetAsync(TEntityKey identity);

        public Task CreateAsync(TEntity entity);

        public Task UpdateAsync(TEntity entity, TEntityKey identity);

        public Task DeleteAsync(TEntityKey identity);
    }
}
