using PizzaAPI.Models;

namespace PizzaAPI.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
