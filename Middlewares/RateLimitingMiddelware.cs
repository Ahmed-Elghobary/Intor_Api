namespace ApiBeginner.Middlewares
{
    public class RateLimitingMiddelware
    {
        private readonly RequestDelegate _next;
        private static int _counter = 0;
        private static DateTime _lastRequestDate = DateTime.Now;

        public RateLimitingMiddelware(RequestDelegate next)
        {
            this._next = next;
          
        }

        public async Task Invoke(HttpContext context)
        {
            _counter++;
            if (DateTime.Now.Subtract(_lastRequestDate).Seconds > 10)
            {
                _counter = 1;
                _lastRequestDate = DateTime.Now;
                _next(context);
            }
            else
            {
                if (_counter > 5)
                {
                    _lastRequestDate= DateTime.Now;
                    await context.Response.WriteAsync("rate limit exceed");
                }
                else
                {
                    _lastRequestDate=DateTime.Now;
                    await _next(context);
                }
            }
        }
    }
}
