using MWI.Core.Messages;

namespace MWI.BitrixPortal.Application.Events
{
    public class BitrixPortalUpdatedEvent : Event
    {
        public Guid BitrixPortalId { get; private set; }
        public string BitrixPortalMemberId { get; private set; }
        public bool InstallStatus { get; private set; }
        public string AccessToken { get; private set; }
        public string ClientEndpoint { get; private set; }

        public BitrixPortalUpdatedEvent(Guid bitrixPortalId, string bitrixPortalMemberId, bool installStatus, string accessToken, string clientEndpoint)
        {
            AggregateId = bitrixPortalId;
            BitrixPortalId = bitrixPortalId;
            BitrixPortalMemberId = bitrixPortalMemberId;
            InstallStatus = installStatus;
            AccessToken = accessToken;
            ClientEndpoint = clientEndpoint;
        }
    }
}
