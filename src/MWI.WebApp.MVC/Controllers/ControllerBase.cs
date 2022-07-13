using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MWI.Core.Communication.Mediator;
using MWI.Core.Messages.CommonMessages.Notifications;
using MWI.WebApp.MVC.Models;
using System.Diagnostics;

namespace MWI.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected ControllerBase(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        protected IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetNotifications().Select(c => c.Value).ToList();
        }

        protected void NotifyError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }

        protected ActionResult CustomResponse(object result = null!)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    sucess = true,
                    data = result
                });
            }
            return BadRequest(new
            {
                sucess = false,
                data = GetErrorMessages()
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErroModelInvalid(modelState);
            return CustomResponse();
        }

        protected void NotifyErroModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError("400", errorMsg);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        protected IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
