using MediatR;
using MWI.BitrixPortal.Application.Events;
using MWI.BitrixPortal.Domain;
using MWI.BitrixPortal.Domain.Entities;
using MWI.Core.Communication.Mediator;
using MWI.Core.Messages;
using MWI.Core.Messages.CommonMessages.Notifications;

namespace MWI.BitrixPortal.Application.Commands
{
    public class BitrixPortalCommandHandler : 
        IRequestHandler<OnAppInstallCommand, bool>,
        IRequestHandler<InstallModulesInBitrixPortalCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IBitrixPortalRepository _portalRepository;
        private readonly IBitrixPortalService _bitrix;
        private readonly DomainNotificationHandler _notifications;

        public BitrixPortalCommandHandler(IMediatorHandler mediatorHandler, 
            IBitrixPortalRepository bitrixPortalRepository, IBitrixPortalService bitrix,
            INotificationHandler<DomainNotification> notifications)
        {
            _mediatorHandler = mediatorHandler;
            _portalRepository = bitrixPortalRepository;
            _bitrix = bitrix;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<bool> Handle(OnAppInstallCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var existingPortal = await _portalRepository.GetPortalByMemberId(command.MemberId);

            if (existingPortal != null!)
            {
                if (existingPortal.ApplicationToken == String.Empty)
                {
                    existingPortal.SetApplicationToken(command.ApplicationToken);
                    _portalRepository.Update(existingPortal);

                    existingPortal.AddDomainEvent(new BitrixPortalUpdatedEvent(existingPortal.Id, existingPortal.MemberId!, existingPortal.InstallStatus,
                        command.AccessToken, existingPortal.ClientEndpoint!));
                }

                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Portal", "Portal already registered"));
                return false;
            }

            var newBitrixPortal = new Portal(command.MemberId, command.Domain, command.Language,
                    command.ApplicationToken, command.BitrixAccountStatus, command.RefreshToken,
                    command.ClientEndpoint, command.ServerEndpoint);

            _portalRepository.Add(newBitrixPortal);

            newBitrixPortal.AddDomainEvent(new RegisteredBitrixPortalEvent(newBitrixPortal.Id, newBitrixPortal.MemberId!, newBitrixPortal.InstallStatus,
                command.AccessToken, newBitrixPortal.ClientEndpoint!));

            return await _portalRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(InstallModulesInBitrixPortalCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var existingPortal = await _portalRepository.GetPortalById(command.Id);

            if (existingPortal == null!)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Portal", "Portal not found"));
                return false;
            }

            // Adiciona o Widget do Canal Aberto com o link da controller que redireciona para view
            var responseAddConnector = await _bitrix.RegisterConnector(command.ClientEndpoint, command.AccessToken);

            if (responseAddConnector is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "Register Connector Not binded"));

            // Realizar o event.bind do OnImConnectorMessageAdd
            var eventBind = await _bitrix.EventBindOnImConnectorMessageAdd(command.ClientEndpoint, command.AccessToken);

            if (eventBind is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "OnImConnectorMessageAdd not binded"));

            // Adiciona como provedor de SMS
            var registerSms = await _bitrix.RegisterSmsConnector(command.ClientEndpoint, command.AccessToken);

            if (registerSms is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "Register SMS not binded"));

            // Criar o Placement Default -> Handler Dashboard
            var placementDefault = await _bitrix.AddPlacementBind(command.ClientEndpoint, command.AccessToken);

            if (placementDefault is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "Default Placement is not binded"));

            // Criar o Placement Deal, Contact, Company, Spa -> Handler Send Template Messages
            var placementDeal = await _bitrix.AddPlacementBind(command.ClientEndpoint, command.AccessToken, "CRM_DEAL_DETAIL_TAB");

            if (placementDeal is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "Deal Placement is not binded"));

            var placementLead = await _bitrix.AddPlacementBind(command.ClientEndpoint, command.AccessToken, "CRM_LEAD_DETAIL_TAB");

            if (placementLead is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "Lead Placement is not binded"));

            var placementContact = await _bitrix.AddPlacementBind(command.ClientEndpoint, command.AccessToken, "CRM_CONTACT_DETAIL_TAB");

            if (placementContact is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "Contact Placement is not binded"));

            var placementCompany = await _bitrix.AddPlacementBind(command.ClientEndpoint, command.AccessToken, "CRM_COMPANY_DETAIL_TAB");

            if (placementCompany is false)
                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Service", "Company Placement is not binded"));

            // Ação no RobotBizProc para envio de msg template Text / Arquivo

            // Adiciona janela de msg template no Chat

            if (_notifications.HasNotifications())
                return false;

            existingPortal.SetInstallStatusTrue();
            _portalRepository.Update(existingPortal);

            return await _portalRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
