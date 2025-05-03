using notification_service.application.dto;
using notification_service.application.dto.enums;
using notification_service.infrastructure.result;
using notification_service.infrastructure.senders.email_sender;
using user_service.services.result.errors.@base;

namespace notification_service.domain.logics;

public class MultiSender(EmailSender emailSender)
{

    public async Task<Result> SendNotifyToUser(Notify notify)
    {
        if(notify is null)
            return Result.Failure(new Error("Notify is null"," Please, check input!"));
        
        switch (notify.PriorityChannel)
        {
            case ChannelNotify.Email:
                return await emailSender.SendNotifyToUser(notify);
            case ChannelNotify.Telegram:
                ; //TODO: Добавить Telegram
                break;
        }

        return Result.Failure(new Error("Channel not found"," Please, check input!"));
    } 
    
}
