namespace Blog;
public static class Configuration
{
    public static string JwtKey = "9f5f6aa778854f2999f91c9e40f81f5c";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_test334";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}