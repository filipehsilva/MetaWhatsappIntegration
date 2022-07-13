using FluentValidation;
using FluentValidation.Results;
using MWI.Core.DomainObjects;

namespace MWI.BitrixPortal.Domain.Entities
{
    public class Portal : Entity, IAggregateRoot
    {
        public string? MemberId { get; private set; }
        public string? Domain { get; private set; }
        public string? Language { get; private set; }
        public string? ApplicationToken { get; private set; }
        public char BitrixAccountStatus { get; private set; }
        public string? AdminUserName { get; private set; }
        public Email? Email { get; private set; }
        public string? RefreshToken { get; private set; }
        public bool Active { get; private set; }
        public bool WizardMode { get; private set; }
        public bool InstallStatus { get; private set; }
        public char PortalStatus { get; private set; }

        protected Portal() { }

        public Portal(string memberId, string domain, string language, string applicationToken, 
            char bitrixAccountStatus, string refreshToken)
        {
            MemberId = memberId;
            Domain = domain;
            Language = language;
            ApplicationToken = applicationToken;
            BitrixAccountStatus = bitrixAccountStatus;
            RefreshToken = refreshToken;
            Active = true;
            WizardMode = true;
            PortalStatus = 'F';
            InstallStatus = false;
        }

        public void Activate() => Active = true;
        public void Disable() => Active = false;

        public void SetInstallStatusTrue() => Active = true;
        public void SetInstallStatusFalse() => Active = false;

        public void SetStatusPaid() => PortalStatus = 'P';
        public void SetStatusTrial() => PortalStatus = 'T';

        public void SetApplicationToken(string appToken) => ApplicationToken = appToken;
        public void UpdateRefreshToken(string refreshToken) => RefreshToken = refreshToken;

        public void SetAdminUsername(string userName) => AdminUserName = userName;
        public void SetEmail(string email) => Email = new Email(email);
        public void SetUsernameAndEmail(string userName, string email)
        {
            AdminUserName = userName;
            Email = new Email(email);
        }

        public ValidationResult DnsBitrixIsValid()
        {
            return new DnsBitrixValidation().Validate(this);
        }
    }

    public class DnsBitrixValidation : AbstractValidator<Portal>
    {
        public DnsBitrixValidation()
        {
            RuleFor(p => p.Domain)
                .Must(domain => domain!.Contains("bitrix24"));
        }
    }

}
