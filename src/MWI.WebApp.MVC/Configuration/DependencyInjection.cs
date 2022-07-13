using MediatR;
using MWI.BitrixPortal.Application.Commands;
using MWI.BitrixPortal.Data;
using MWI.BitrixPortal.Data.Repository;
using MWI.BitrixPortal.Domain;
using MWI.Core.Communication.Mediator;
using MWI.Core.Messages.CommonMessages.Notifications;

namespace MWI.WebApp.MVC.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Bitrix Portal
            services.AddScoped<BitrixPortalContext>();
            services.AddScoped<IBitrixPortalRepository, BitrixPortalRepository>();
            services.AddScoped<IRequestHandler<OnAppInstallCommand, bool>, BitrixPortalCommandHandler>();
        }
    }
}
