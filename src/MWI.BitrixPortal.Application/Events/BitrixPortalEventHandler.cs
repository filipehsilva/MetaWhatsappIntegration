using MediatR;
using MWI.BitrixPortal.Application.Commands;
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

        public async Task Handle(RegisteredBitrixPortalEvent notification, CancellationToken cancellationToken)
        {
            if (!notification.InstallStatus)
                await _mediatorHandler.SendCommand(new InstallModulesInBitrixPortalCommand(notification.BitrixPortalId,
                    notification.BitrixPortalMemberId, notification.AccessToken, notification.ClientEndpoint));
        }

        public async Task Handle(BitrixPortalUpdatedEvent notification, CancellationToken cancellationToken)
        {
            if (!notification.InstallStatus)
                await _mediatorHandler.SendCommand(new InstallModulesInBitrixPortalCommand(notification.BitrixPortalId,
                    notification.BitrixPortalMemberId, notification.AccessToken, notification.ClientEndpoint));
        }
    }
}
