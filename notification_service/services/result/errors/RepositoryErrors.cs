using user_service.services.result.errors.@base;

namespace notification_service.infrastructure.result.errors;

public class RepositoryErrors
{
    public Error NotFound = new("Repository.NotFound", "Data is not found!");
}
