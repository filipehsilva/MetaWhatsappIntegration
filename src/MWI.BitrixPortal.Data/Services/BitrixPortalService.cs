using Bitrix24.Connector;
using MWI.BitrixPortal.Domain;
using MWI.BitrixPortal.Domain.DTO;
using MWI.Core.Helpers;

namespace MWI.BitrixPortal.Data.Services
{
    public class BitrixPortalService : IBitrixPortalService
    {
        public async Task<bool> EventBindOnImConnectorMessageAdd(string clientEndpoint, string accessToken)
        {
            var connector = new Bitrix24Connector(clientEndpoint);

            var body = Bitrix24QueryBuilder
                .Create()
                .AddRequestParam("auth", accessToken)
                .AddRequestParam("event", "OnImConnectorMessageAdd")
                .AddRequestParam("handler", Settings.OnImMessageAddHandler)
                .Build();

            var response = await connector.RequestHandler.ExecuteAsync<BitrixBoolResponseDto>("event.bind", body);

            if (response.ErrorDescription != null) return false;

            return true;
        }

        public async Task<bool> RegisterConnector(string clientEndpoint, string accessToken)
        {
            var connector = new Bitrix24Connector(clientEndpoint);

            var options = new ConnectorRegisterDto(accessToken);

            var body = Bitrix24QueryBuilder
                .Create()
                .AddParams(options.ToDictionary())
                .Build();

            var response = await connector.RequestHandler.ExecuteAsync<BitrixBoolResponseDto>("imconnector.register", body);

            if (response.ErrorDescription != null) return false;

            return true;
        }

        public async Task<bool> RegisterSmsConnector(string clientEndpoint, string accessToken)
        {
            var connector = new Bitrix24Connector(clientEndpoint);

            var body = Bitrix24QueryBuilder
                .Create()
                .AddRequestParam("auth", accessToken)
                .AddRequestParam("code", Settings.SmsConnectorId)
                .AddRequestParam("type", "SMS")
                .AddRequestParam("handler", Settings.SmsConnectorHandler)
                .AddRequestParam("name", "Meta Whatsapp Integration")
                .AddRequestParam("description", "Send messages to whatsapp")
                .Build();

            var response = await connector.RequestHandler.ExecuteAsync<BitrixBoolResponseDto>("messageservice.sender.add", body);

            if (response.ErrorDescription != null) return false;

            return true;
        }

        public async Task<bool> AddPlacementBind(string clientEndpoint, string accessToken, string placement = null)
        {
            var connector = new Bitrix24Connector(clientEndpoint);

            if (placement is null)
            {
                var query = Bitrix24QueryBuilder
                    .Create()
                    .AddRequestParam("auth", accessToken)
                    .AddRequestParam("placement", "DEFAULT")
                    .AddRequestParam("handler", Settings.PlacementBindDefaultHandler)
                    .AddRequestParam("title", "Meta Whatsapp Integration")
                    .Build();

                var response = await connector.RequestHandler.ExecuteAsync<BitrixBoolResponseDto>("placement.bind", query);

                if (response.ErrorDescription != null) return false;

                return true;

            } else
            {
                var query = Bitrix24QueryBuilder
                    .Create()
                    .AddRequestParam("auth", accessToken)
                    .AddRequestParam("placement", placement)
                    .AddRequestParam("handler", Settings.PlacementBindSendHandler)
                    .AddRequestParam("title", "WhatsApp")
                    .Build();

                var response = await connector.RequestHandler.ExecuteAsync<BitrixBoolResponseDto>("placement.bind", query);

                if (response.ErrorDescription != null) return false;

                return true;
            }
        }
    }
}
