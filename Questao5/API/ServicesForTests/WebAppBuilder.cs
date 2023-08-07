using Microsoft.AspNetCore.Mvc.Testing;

namespace API.ServicesForTests
{
    public class WebAppBuilder<T> : WebApplicationFactory<T> where T : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
    }
}
