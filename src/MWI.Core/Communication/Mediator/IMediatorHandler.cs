using MWI.Core.Messages;
using MWI.Core.Messages.CommonMessages.Notifications;

namespace MWI.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T eventMessage) where T : Event;
        Task<bool> SendCommand<T>(T commandMessage) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
