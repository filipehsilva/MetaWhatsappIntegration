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

        public BitrixPortalCommandHandler(IMediatorHandler mediatorHandler, IBitrixPortalRepository bitrixPortalRepository)
        {
            _mediatorHandler = mediatorHandler;
            _portalRepository = bitrixPortalRepository;
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

                    existingPortal.AddDomainEvent(new BitrixPortalUpdatedEvent(existingPortal.Id, existingPortal.MemberId!));
                }

                await _mediatorHandler.PublishNotification(new DomainNotification("Bitrix Portal", "Portal already registered"));
                return false;
            }

            var newBitrixPortal = new Portal(command.MemberId, command.Domain, command.Language,
                    command.ApplicationToken, command.BitrixAccountStatus, command.RefreshToken,
                    command.ClientEndpoint, command.ServerEndpoint);

            _portalRepository.Add(newBitrixPortal);

            newBitrixPortal.AddDomainEvent(new RegisteredBitrixPortalEvent(newBitrixPortal.Id, newBitrixPortal.MemberId!));

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


            // Aplicar o RetryPattern (Polly)

            // Adiciona o Widget do Canal Aberto

            // Adiciona como provedor de SMS

            // Criar o Placement Default -> Handler Dashboard

            // Criar o Placement Deal, Contact, Company, Spa -> Handler Send Template Messages

            // Ação no RobotBizProc para envio de msg template Text / Arquivo

            // Adiciona janela de msg template no Chat

            return true;
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
