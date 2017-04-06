
namespace CashJobSite.Application.Services
{
    public interface IEmailService
    {
        void SendEmail(string toEmailAddress, string emailSubject, string emailBody);
    }
}
