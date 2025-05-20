using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Identity;

public class EmailService : IEmailSender<ApplicationUser>
{
	public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
	{
		return SendEmailAsync(email, "Confirm your email", $"<a href='{confirmationLink}'>Click here to confirm</a>");
	}

	public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
	{
		return SendEmailAsync(email, "Reset your password", $"<a href='{resetLink}'>Click here to reset your password</a>");
	}

	public Task SendPasswordChangedConfirmationAsync(ApplicationUser user, string email)
	{
		return SendEmailAsync(email, "Password changed", "Your password has been successfully changed.");
	}

	public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
	{
		return SendEmailAsync(email, "Password reset code", $"Your password reset code is: <strong>{resetCode}</strong>");
	}

	private Task SendEmailAsync(string toEmail, string subject, string htmlBody)
	{
		Console.WriteLine($"To: {toEmail}, Subject: {subject}, Body: {htmlBody}");
		return Task.CompletedTask;
	}
}
