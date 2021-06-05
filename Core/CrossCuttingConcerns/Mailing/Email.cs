namespace Core.CrossCuttingConcerns.Mailing
{
    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Body { get; set; }


        public Email(string @from, string to, string subject, bool isBodyHtml, string body)
        {
            From = @from;
            To = to;
            Subject = subject;
            IsBodyHtml = isBodyHtml;
            Body = body;
        }
    }
}