namespace InFatec.API.Util.Interfaces
{
    public interface IEmailUtil
    {
        public Task<bool> SendEmail(string email, string subject, string body);
    }
}
