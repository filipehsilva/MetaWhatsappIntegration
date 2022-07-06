using MediatR;
using MWI.Core.Messages;
using MWI.Core.Messages.CommonMessages.Notifications;

namespace MWI.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> SendCommand<T>(T commandMessage) where T : Command
        {
            return await _mediator.Send(commandMessage);
        }

        public async Task PublishEvent<T>(T eventMessage) where T : Event
        {
            await _mediator.Publish(eventMessage);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}
