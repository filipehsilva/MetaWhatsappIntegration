using FluentValidation;
using FluentValidation.Results;
using MWI.Core.DomainObjects;
using NetDevPack.Domain;

namespace MWI.BitrixPortal.Domain.Entities
{
    public class Portal : Entity, IAggregateRoot
    {
        public string MemberId { get; private set; } = string.Empty;
        public string Domain { get; private set; } = string.Empty;
        public string Language { get; private set; } = string.Empty;
        public string ApplicationToken { get; private set; } = string.Empty;
        public AccountStatusEnum BitrixAccountStatus { get; private set; }
        public string AdminUserName { get; private set; } = string.Empty;
        public Email? Email { get; private set; }
        public string RefreshToken { get; private set; } = string.Empty;
        public bool Active { get; private set; }
        public bool WizardMode { get; private set; }
        public AccountStatusEnum PortalStatus { get; private set; }

        protected Portal() { }

        public Portal(string memberId, string domain, string language, string applicationToken, 
            AccountStatusEnum bitrixAccountStatus, string refreshToken)
        {
            MemberId = memberId;
            Domain = domain;
            Language = language;
            ApplicationToken = applicationToken;
            BitrixAccountStatus = bitrixAccountStatus;
            RefreshToken = refreshToken;
            Active = true;
            WizardMode = true;
            PortalStatus = AccountStatusEnum.Free;
        }

        public void Activate() => Active = true;

        public void Disable() => Active = false;

        public void SetStatusPaid() => PortalStatus = AccountStatusEnum.Paid;

        public void SetStatusTrial() => PortalStatus = AccountStatusEnum.Trial;

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
                .Must(domain => domain.Contains("bitrix24"));
        }
    }

}
