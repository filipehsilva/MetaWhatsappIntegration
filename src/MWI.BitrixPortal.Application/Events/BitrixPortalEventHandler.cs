using MediatR;
using MWI.Core.Communication.Mediator;

namespace MWI.BitrixPortal.Application.Events
{
    public class BitrixPortalEventHandler :
        INotificationHandler<RegisteredBitrixPortalEvent>,
        INotificationHandler<BitrixPortalUpdatedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BitrixPortalEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(RegisteredBitrixPortalEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(BitrixPortalUpdatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
