namespace Core.CrossCuttingConcerns.Mailing
{
    public class Credentials
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int? Port { get; set; }
        public string? Host { get; set; }

        public Credentials(string emailAddress, string password, int? port, string? host)
        {
            EmailAddress = emailAddress;
            Password = password;
            Port = port;
            Host = host;
        }
    }
}