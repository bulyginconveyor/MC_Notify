using user_service.services.result.errors.@base;

namespace notification_service.infrastructure.result;

public static class ResultExtensions
{
    public static T Match<T>(
        this Result result,
        Func<T> success,
        Func<Error, T> failure)
        => result.IsSuccess ? success() : failure(result.Error!);
}
