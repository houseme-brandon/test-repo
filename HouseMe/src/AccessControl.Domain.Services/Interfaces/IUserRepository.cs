using System;
using System.Threading.Tasks;
using AccessControl.Domain.UserAggregate;
using Shared.Domain.ValueObjects;

namespace AccessControl.Domain.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(Guid id);
        Task<bool> UserExistsAsync(RsaIdNumber idNumber);
        Task<bool> IdNumberExistsAsync(RsaIdNumber idNumber);
        Task<bool> ConfirmEmailAddress(Guid id, string code);
        Task<bool> EmailConfirmed(RsaIdNumber id);
        Task CreateUserAsync(User user, string password);
        Task<string> GenerateConfirmationUrlAsync(User user, CodedUrlBuilder builder);
        Task SignInUser(Guid id);
        Task PasswordLogin(RsaIdNumber id, string password);
    }
}
    