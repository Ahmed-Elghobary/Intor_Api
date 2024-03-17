using AspNetCoreRateLimit;
namespace ApiBeginner.Middlewares
{
    public interface IRateLimitCounterRepository
    {
        /// <summary>
        /// Retrieves the rate limit counter for the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier for the rate limit counter.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the rate limit counter if found; otherwise, null.</returns>
        Task<RateLimitCounter> GetAsync(string id);

        /// <summary>
        /// Sets the rate limit counter for the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier for the rate limit counter.</param>
        /// <param name="counter">The rate limit counter to set.</param>
        /// <param name="expirationTime">The expiration time for the rate limit counter.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SetAsync(string id, RateLimitCounter counter, TimeSpan expirationTime);
    }
}

