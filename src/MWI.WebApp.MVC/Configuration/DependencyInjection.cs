using MWI.Core.Communication.Mediator;
using MWI.Core.Messages.CommonMessages.IntegrationEvents;
using MWI.Core.Messages.CommonMessages.Notifications;

namespace MWI.WebApp.MVC.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}
