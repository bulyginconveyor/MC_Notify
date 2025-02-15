using System.Text.RegularExpressions;

namespace notification_service.domain.models.valueobjects;

public record Email
{
    public string EmailAddress { get; private set; }
    public DateTime? ConfirmEmail { get; private set; }

    public static Email Create(string emailAddress)
    {
        return new Email
        {
            EmailAddress = emailAddress,
            ConfirmEmail = null
        };
    }

    public static bool ValidateEmailAddress(string emailAddress)
        => Regex.IsMatch(emailAddress, @"^[a-zA-Z0-9_%+-]+@[a-zA-Z0-9-]{2,}+\.[a-zA-Z]{2,}$");
    
    public void Confirm() => ConfirmEmail = DateTime.UtcNow;
    public bool IsConfirmed() => ConfirmEmail != null;
    public bool IsNotConfirmed() => ConfirmEmail == null;

    public void ChangeEmail(string emailAddress)
    {
        EmailAddress = emailAddress;
    }
}
