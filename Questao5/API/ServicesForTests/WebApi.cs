namespace API.ServicesForTests
{
    public static class WebApi
    {
        public static HttpClient Client
        {
            get
            {
                return new WebAppBuilder<Program>().CreateClient();
            }
        }
    }
}
