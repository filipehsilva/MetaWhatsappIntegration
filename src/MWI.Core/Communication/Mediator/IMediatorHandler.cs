using FluentValidation.Results;
using MWI.Core.Messages.CommonMessages.Notifications;
using NetDevPack.Messaging;

namespace MWI.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T comando) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
