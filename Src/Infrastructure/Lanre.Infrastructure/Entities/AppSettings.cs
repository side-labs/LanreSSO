namespace Lanre.Infrastructure.Entities
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Database Database { get; set; }
        public Settings Settings { get; set; }
        public HttpsConfig HttpsConfig { get; set; }
    }


    public class HttpsConfig
    {
        public int Port { get; set; }
    }


    public class ConnectionStrings
    {
        public string Lanre { get; set; }
    }

    public class Database
    {
        public bool UseInMemory { get; set; }
        public bool Migrate { get; set; }
        public bool EnsureDeleted { get; set; }
    }

    public class Settings
    {
        public bool DetailedErrors { get; set; }
    }


}
