using Microsoft.AspNetCore.Mvc;

namespace LineBot.Controllers
{
	public class LineBotController : Controller
	{
		private readonly string channelAccessToken = "Your channel access token";
		private readonly string channelSecret = "Your channel secret";

		public LineBotController()
		{

		}
		[HttpPost("Webhook")]
		public IActionResult Webhook()
		{
			return Ok();
		}
	}
}
