using MediatR;
using MWI.BitrixPortal.Data;
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
        }
    }
}
