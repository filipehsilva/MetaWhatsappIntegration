using FluentValidation;
using MWI.Core.Messages;

namespace MWI.BitrixPortal.Application.Commands
{
    public class InstallModulesInBitrixPortalCommand : Command
    {
        public Guid Id { get; private set; }
        public string MemberId { get; private set; }
        public string AccessToken { get; private set; }
        public string ClientEndpoint { get; private set; }

        public InstallModulesInBitrixPortalCommand(Guid id, string memberId, string accessToken, string clientEndpoint)
        {
            Id = id;
            MemberId = memberId;
            AccessToken = accessToken;
            ClientEndpoint = clientEndpoint;
        }

        public override bool IsValid()
        {
            ValidationResult = new InstallModulesInBitrixPortalValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class InstallModulesInBitrixPortalValidation : AbstractValidator<InstallModulesInBitrixPortalCommand>
    {
        public InstallModulesInBitrixPortalValidation()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Id is empty");

            RuleFor(p => p.MemberId)
                .NotEmpty()
                .WithMessage("Member is empty");

            RuleFor(p => p.ClientEndpoint)
                .NotEmpty()
                .WithMessage("ClientEndpoint is empty");
        }
    }
}
