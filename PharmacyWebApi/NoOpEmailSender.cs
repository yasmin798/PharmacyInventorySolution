using Microsoft.AspNetCore.Identity;
using PharmacyData.Entities;

namespace PharmacyWebApi
{
    public class NoOpEmailSender : IEmailSender<ApplicationUser>
    {
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            // In a real app, you'd send an actual email here.
            // For this project, we just log it to the console for demo purposes.
            Console.WriteLine($"[DEV] Confirmation link for {email}: {confirmationLink}");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            Console.WriteLine($"[DEV] Password reset link for {email}: {resetLink}");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            Console.WriteLine($"[DEV] Password reset code for {email}: {resetCode}");
            return Task.CompletedTask;
        }
    }
}