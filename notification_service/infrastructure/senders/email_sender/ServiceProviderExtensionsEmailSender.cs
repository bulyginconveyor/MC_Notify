using EmailSenderSMTP;
using static System.String;

namespace notification_service.infrastructure.senders.email_sender;

public static class ServiceProviderExtensionsEmailSender
{
    public static void AddEmailSender(this IServiceCollection services)
    {
        var mailServiceStr = Environment.GetEnvironmentVariable("MAIL_SERVICE");
        var emailSend = Environment.GetEnvironmentVariable("EMAIL_SENDER");
        var emailSendPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

        if(IsNullOrWhiteSpace(mailServiceStr) || IsNullOrWhiteSpace(emailSend) || IsNullOrWhiteSpace(emailSendPassword))
            throw new Exception("EmailSender is not configured");
        
        MailService mailService = (MailService)Enum.Parse(typeof(MailService), mailServiceStr);
        
        SenderSMTP sender = new SenderSMTP(
            mailService,
            emailSend,
            emailSendPassword
            );
        
        services.AddSingleton<SenderSMTP>(sender);
        services.AddSingleton<EmailSender>();
    }
}
