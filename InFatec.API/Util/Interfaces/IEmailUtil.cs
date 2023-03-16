namespace InFatec.API.Util.Interfaces
{
    public interface IEmailUtil
    {
        public bool SendEmail(string email, string subject, string body);
    }
}
