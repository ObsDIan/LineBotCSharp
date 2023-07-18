using LineBotMessage.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using LineBotMessage.Enum;
using LineBotMessage.Providers;
using LineBotMessage.Dtos.Webhook;

namespace LineBotMessage.Controllers
{
	[Route("api/[Controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {


		private readonly LineBotService _lineBotService;
		// constructor
		public LineBotController()
		{
			_lineBotService = new LineBotService();
		}

		[HttpPost("Webhook")]
        public async Task<IActionResult> Webhook(WebhookRequestBodyDto body)
        {
            await _lineBotService.ReceiveWebhook(body);
            return Ok();
        }

        [HttpPost("SendMessage/Broadcast")]
        public IActionResult Broadcast([Required] string messageType, object body)
        {
            _lineBotService.BroadcastMessageHandler(messageType, body);
            return Ok();
        }
    }
}