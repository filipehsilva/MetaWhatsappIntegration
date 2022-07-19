namespace MWI.BitrixPortal.Domain
{
    public interface IBitrixPortalService
    {
        Task<bool> RegisterConnector(string clientEndpoint, string accessToken);
        Task<bool> EventBindOnImConnectorMessageAdd(string clientEndpoint, string accessToken);
        Task<bool> RegisterSmsConnector(string clientEndpoint, string accessToken);
        Task<bool> AddPlacementBind(string clientEndpoint, string accessToken, string placement = null!);
    }
}
