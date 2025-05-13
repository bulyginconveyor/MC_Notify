using EmailSenderSMTP;
using notification_service.application.dto;
using notification_service.infrastructure.result;

namespace notification_service.infrastructure.senders.email_sender;

public class EmailSender(SenderSMTP senderSmtp)
{
    private readonly string _appAddress = Environment.GetEnvironmentVariable("APP_ADDRESS") ??
                                          throw new Exception("APP_ADDRESS not set!");
    
    public async Task<Result> SendEmailConfirmEmail(string registerDataEmail, string code)
    {
        var confirmMessage = CreateConfrimMessage(code);
        
        /*
         var confirmLink = CreateConfirmLink(registerDataEmail, code);
         var body =
            $"""
             <html>
                <body>
                    <h1>Добро пожаловать на MoneyCushion.App!</h1>
                    <p>Для подтверждения регистрации на MoneyCushion.App перейдите по ссылке:
                        <a href='{confirmLink}'>Подтвердить регистрацию</a>
                    </p>
                </body>
             </html>
             """;

        var message = senderSmtp.CreateMailMessageBodyIsHTML(
            registerDataEmail,
            "MoneyCushion.App",
            "Подтверждение регистрации на MoneyCushion.App",
            body
        );*/

        var message = senderSmtp.CreateMailMessageBodyIsText(
            registerDataEmail,
            "MoneyCushion.App",
            "Подтверждение регистрации на MoneyCushion.App",
            confirmMessage
        );
        
        await senderSmtp.SendMailAsync(message);
        
        return Result.Success();
    }

    public string CreateConfrimMessage(string code)
        => $"Код потверждения регистрации: {code}";
    
    private string CreateConfirmLink(string registerDataEmail, string code) 
        => $"{_appAddress}/api/confirm_email?email={registerDataEmail}&code={code}";

    public async Task<Result> SendNotifyToUser(Notify notify)
    {
        //TODO: Добавить метод для отправки уведомления пользователю
        throw new NotImplementedException();
    }
}
