namespace TaskBoard.Application;

public interface IEmailSendler
{
    Task SendAsync(string to, string confirmationCode);
}