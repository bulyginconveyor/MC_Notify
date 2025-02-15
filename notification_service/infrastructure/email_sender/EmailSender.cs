using EmailSenderSMTP;
using notification_service.infrastructure.result;
using user_service.services.result;

namespace notification_service.services.email_sender;

public class EmailSender(SenderSMTP senderSmtp)
{
    private string _appAddress = Environment.GetEnvironmentVariable("APP_ADDRESS");
    
    //TODO: Сделать
    public async Task<Result> SendEmailConfirmEmail(string registerDataEmail, string code)
    {
        var confirmLink = CreateConfirmLink(registerDataEmail, code);
        
        await senderSmtp.SendMailAsync(
            registerDataEmail,
            "MoneyCushion.App",
            "Подтверждение регистрации на MoneyCushion.App",
            $"Для подтверждения регистрации на MoneyCushion.App перейдите по ссылке:\n\n {confirmLink}"
        );
        
        return Result.Success();
    }

    private string CreateConfirmLink(string registerDataEmail, string code)
    {
        return $"{_appAddress}/api/confirm?email={registerDataEmail}&code={code}";
    }
}
