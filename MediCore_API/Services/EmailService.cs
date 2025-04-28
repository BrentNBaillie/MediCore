using Microsoft.AspNetCore.Identity;

public class EmailService : IEmailSender<IdentityUser>
{
	public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
	{
		return SendEmailAsync(email, "Confirm your email", $"<a href='{confirmationLink}'>Click here to confirm</a>");
	}

	public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
	{
		return SendEmailAsync(email, "Reset your password", $"<a href='{resetLink}'>Click here to reset your password</a>");
	}

	public Task SendPasswordChangedConfirmationAsync(IdentityUser user, string email)
	{
		return SendEmailAsync(email, "Password changed", "Your password has been successfully changed.");
	}

	public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
	{
		return SendEmailAsync(email, "Password reset code", $"Your password reset code is: <strong>{resetCode}</strong>");
	}

	private Task SendEmailAsync(string toEmail, string subject, string htmlBody)
	{
		Console.WriteLine($"To: {toEmail}, Subject: {subject}, Body: {htmlBody}");
		return Task.CompletedTask;
	}
}
