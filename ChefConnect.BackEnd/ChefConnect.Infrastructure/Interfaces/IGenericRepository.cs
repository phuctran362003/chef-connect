namespace ChefConnect.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Retrieve all entities
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        // Create operations
        void Create(T entity);
        Task<int> CreateAsync(T entity);

        // Update operations
        void Update(T entity);
        Task<int> UpdateAsync(T entity);

        // Remove operations
        bool Remove(T entity);
        Task<bool> RemoveAsync(T entity);

        // Retrieve entity by ID (int, string, Guid)
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        T GetById(string code);
        Task<T> GetByIdAsync(string code);

        T GetById(Guid code);
        Task<T> GetByIdAsync(Guid code);

        // Methods for prepare operations (without saving)
        void PrepareCreate(T entity);
        void PrepareUpdate(T entity);
        void PrepareRemove(T entity);

        // Save changes
        int Save();
        Task<int> SaveAsync();
    }
}
