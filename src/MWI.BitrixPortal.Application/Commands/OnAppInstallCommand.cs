using FluentValidation;
using MWI.Core.Messages;

namespace MWI.BitrixPortal.Application.Commands
{
    public class OnAppInstallCommand : Command
    {
        public string Event { get; private set; }
        public string MemberId { get; private set; }
        public string Domain { get; private set; }
        public string Language { get; private set; }
        public string ApplicationToken { get; private set; }
        public char BitrixAccountStatus { get; private set; }
        public string RefreshToken { get; private set; }
        public string ClientEndpoint { get; private set; }
        public string ServerEndpoint { get; private set; }

        public OnAppInstallCommand(string @event, string memberId, string domain, string language,
            string applicationToken, char bitrixAccountStatus, string refreshToken, string clientEndpoint, string serverEndpoint)
        {
            Event = @event;
            MemberId = memberId;
            Domain = domain;
            Language = language;
            ApplicationToken = applicationToken;
            BitrixAccountStatus = bitrixAccountStatus;
            RefreshToken = refreshToken;
            ClientEndpoint = clientEndpoint;
            ServerEndpoint = serverEndpoint;
        }

        public override bool IsValid()
        {
            ValidationResult = new OnAppInstallValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class OnAppInstallValidation : AbstractValidator<OnAppInstallCommand>
    {
        public OnAppInstallValidation()
        {
            RuleFor(p => p.Domain)
                .Must(domain => domain.Contains("bitrix24"))
                .WithMessage("Domain not authorized");

            RuleFor(p => p.MemberId)
                .NotEmpty()
                .WithMessage("Member is empty");

            RuleFor(p => p.Event)
                .Equal("ONAPPINSTALL")
                .WithMessage("Bad Event");

            RuleFor(p => p.ApplicationToken)
                .NotEmpty()
                .WithMessage("AppToken is empty");

            RuleFor(p => p.RefreshToken)
                .NotEmpty()
                .WithMessage("RefToken is empty");

            RuleFor(p => p.ClientEndpoint)
                .NotEmpty()
                .WithMessage("ClientEndpoint is empty");

            RuleFor(p => p.ServerEndpoint)
                .NotEmpty()
                .WithMessage("ServerEndpoint is empty");
        }
    }
}
