using MediatR;
using Microsoft.AspNetCore.Mvc;
using MWI.BitrixPortal.Application.Commands;
using MWI.Core.Communication.Mediator;
using MWI.Core.Messages.CommonMessages.Notifications;
using MWI.WebApp.MVC.Models.BitrixPortal;

namespace MWI.WebApp.MVC.Controllers
{
    [Route("v1/bitrixportal")]
    [ApiController]
    public class BitrixPortalController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BitrixPortalController(INotificationHandler<DomainNotification> notifications, 
                                      IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [Route("onappinstall")]
        [HttpPost]
        public async Task<IActionResult> OnAppInstall([FromBody] OnAppInstallInputModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var command = new OnAppInstallCommand(model.Event, model.Auth.Member_Id, model.Auth.Domain, 
                model.Data.Language_Id, model.Auth.Application_Token, model.Auth.Status, model.Auth.Refresh_Token,
                model.Auth.Client_Endpoint, model.Auth.Server_Endpoint);

            await _mediatorHandler.SendCommand(command);

            return CustomResponse("Bitrix Portal registered successfully!");
        }
    }
}
