using Bitrix24.Connector;
using MediatR;
using MWI.BitrixPortal.Application.Commands;
using MWI.BitrixPortal.Application.Events;
using MWI.BitrixPortal.Data;
using MWI.BitrixPortal.Data.Repository;
using MWI.BitrixPortal.Data.Services;
using MWI.BitrixPortal.Domain;
using MWI.Core.Communication.Mediator;
using MWI.Core.Messages.CommonMessages.Notifications;

namespace MWI.WebApp.MVC.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Bitrix
            services.AddHttpClient<Bitrix24Connector>();

            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Bitrix Portal
            services.AddScoped<BitrixPortalContext>();
            services.AddScoped<IBitrixPortalRepository, BitrixPortalRepository>();
            services.AddScoped<IBitrixPortalService, BitrixPortalService>();
            //commands
            services.AddScoped<IRequestHandler<OnAppInstallCommand, bool>, BitrixPortalCommandHandler>();
            services.AddScoped<IRequestHandler<InstallModulesInBitrixPortalCommand, bool>, BitrixPortalCommandHandler>();
            //events
            services.AddScoped<INotificationHandler<RegisteredBitrixPortalEvent>, BitrixPortalEventHandler>();
            services.AddScoped<INotificationHandler<BitrixPortalUpdatedEvent>, BitrixPortalEventHandler>();
        }
    }
}
