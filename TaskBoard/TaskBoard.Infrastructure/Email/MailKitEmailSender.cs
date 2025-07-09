using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TaskBoard.Application;

namespace TaskBoard.Infrastructure;

public class MailKitEmailSender: IEmailSendler
{
    private readonly IConfiguration _configuration;
    
    public MailKitEmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendAsync(string to, string confirmationCode)
    {
        var fromEmail = _configuration["EmailSettings:From"];
        var password = _configuration["EmailSettings:Password"];
        
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("TaskBoard", "TaskBoard")); // или из конфига
        emailMessage.To.Add(MailboxAddress.Parse(to));
        emailMessage.Subject = "Подтверждение адреса электронной почты";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = "<html>"+"<head>"+"<style>"+
                                    "body{font-family:Arial,sans-serif; background-color:#f2f2f2;}"+
                                    ".container{max-width: 600px;margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1)}"+
                                    ".header {text-alight: center; margin-bottom: 20px;}"+
                                    ".message {font-size: 16px; line-height: 1.6;}"+
                                    ".container-code {background-color: #f0f0f0; padding: 5px; border-radius: 5px; font-weight: bold; }"+
                                    ".code {text-alight: center;}"+
                                    "</style>"+
                                    "</head>"+
                                    "<body>"+
                                    "<div class='container'>"+
                                    "<div class='header'><h1>Для прохождения регистрации введите код регистрации, не передавайте его никому. Если вы не регистрировались, проигнорируйте данное письмо.</h1></div>"+
                                    "<div class='message'>"+
                                    "<p>Ваш код:</p>"+
                                    "<div class='container-code'><p class='code'>" + confirmationCode + "</p></div>"+
                                    "</div>"+"</div>"+"</body>"+"</html>"
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 465, true);
        await client.AuthenticateAsync(fromEmail, password);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}