using MWI.Core.Messages;

namespace MWI.BitrixPortal.Application.Events
{
    public class BitrixPortalUpdatedEvent : Event
    {
        public Guid BitrixPortalId { get; private set; }
        public string BitrixPortalMemberId { get; private set; } = string.Empty;

        public BitrixPortalUpdatedEvent(Guid bitrixPortalId, string bitrixPortalMemberId)
        {
            AggregateId = bitrixPortalId;
            BitrixPortalId = bitrixPortalId;
            BitrixPortalMemberId = bitrixPortalMemberId;
        }
    }
}
