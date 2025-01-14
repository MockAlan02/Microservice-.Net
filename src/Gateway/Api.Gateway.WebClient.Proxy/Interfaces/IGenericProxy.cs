using Service.Common.Collection;

namespace Api.Gateway.WebClient.Proxy.Interfaces
{
    /// <summary>
    /// Defines a generic proxy interface for managing entities of type <typeparamref name="TDto"/> 
    /// using commands of type <typeparamref name="TCommand"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command object used for creating entities.</typeparam>
    /// <typeparam name="TDto">The type of the data transfer object representing entities.</typeparam>
    /// <typeparam name="TKey">The type of the key used to identify entities.</typeparam>
    public interface IGenericProxy<TCommand, TDto, TKey>
        where TCommand : class
        where TDto : class
    {
        /// <summary>
        /// Retrieves a paginated collection of entities.
        /// </summary>
        /// <param name="page">The page number to retrieve (starting at 1).</param>
        /// <param name="take">The number of items to retrieve per page.</param>
        /// <param name="selects">An optional collection of IDs to filter the results.</param>
        /// <returns>A task representing the asynchronous operation. The result contains a <see cref="DataCollection{TDto}"/> with the retrieved entities.</returns>
        Task<DataCollection<TDto>> GetAllAsync(int page, int take, IEnumerable<int> selects = null);

        /// <summary>
        /// Retrieves a single entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The result contains the retrieved entity of type <typeparamref name="TDto"/>.</returns>
        Task<TDto> GetAsync(TKey id);

        /// <summary>
        /// Creates a new entity using the provided command.
        /// </summary>
        /// <param name="command">The command object containing the data for the new entity.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateAsync(TCommand command);
    }
}
