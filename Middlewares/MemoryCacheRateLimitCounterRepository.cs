using AspNetCoreRateLimit;
namespace ApiBeginner.Middlewares
{
    public class MemoryCacheRateLimitCounterRepository : IRateLimitCounterRepository
    {
        private readonly Dictionary<string, RateLimitCounter> _counters = new Dictionary<string, RateLimitCounter>();

        public Task<RateLimitCounter> GetAsync(string id)
        {
            lock (_counters)
            {
                if (_counters.ContainsKey(id))
                {
                    return Task.FromResult(_counters[id]);
                }
                else
                {
                    return Task.FromResult(new RateLimitCounter());
                }
            }
        }

        public Task SetAsync(string id, RateLimitCounter counter, TimeSpan expirationTime)
        {
            lock (_counters)
            {
                _counters[id] = counter;
            }
            return Task.CompletedTask;
        }
    }
}

