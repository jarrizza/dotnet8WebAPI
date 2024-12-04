namespace WebAPIDemo.Authority
{
    public static class AppRepository
    {
        private static List<Application> applications = new List<Application>()
        {
            new Application {
                ApplicationId = 1,
                Name = "MVCWebApp",
                ClientId = "53D2C1E7-4587-4AD5-8C6E-A8E4BD58940E",
                Secret = "0673FC70-4587-4AD5-B4A3-A8E4BD58940E",
                Scopes = "read,write,delete"
                }
        };

        public static Application? GetApplicationByClientId(string clientId)
        {
            return applications.FirstOrDefault(app => app.ClientId == clientId);
        }

    }
}
