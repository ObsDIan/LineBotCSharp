using HKDB.Data;
using AskApi.Services;
using Microsoft.AspNetCore.Mvc;
using AskApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AskApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly HKContext _ctx;
		private readonly TranslateService _tr;
		private readonly SimService _sim;
		private readonly GptService _gpt;
		public WeatherForecastController(HKContext context, TranslateService tr, GptService gpt, SimService sim)
		{
			_ctx = context;
			_tr = tr;
			_gpt = gpt;
			_sim = sim;
		}

		[Route("Anser")]
		[HttpPost]
		public async Task<string> SendMessage(string input)
		{
			var lan = await _tr.GetLunguage(input);
			var messagetr = input;
			if (lan != "en")
			{
				messagetr = await _tr.GetTranslate(input, lan, "en");
			}
			var inputEmbedding = await _gpt.GetEmbedding(messagetr);

			var embeddings = _ctx.Embeddings.ToList();

			var SimDatas = await _sim.SimMethod(inputEmbedding, embeddings);

			SimDatas = SimDatas.OrderBy(x => x.Similarity > 0.8).Take(5).ToList();

			if (SimDatas.Count == 0)
			{
				return "無相關問題!!";
			}
			var container = new List<string>();

			foreach (var s in SimDatas)
			{
				var temp = await _ctx.Embeddings.FirstAsync(x => x.EmbeddingId == s.DbId);
				container.Add(temp.Qa);
			}
			var send = new EmbViewModel()
			{
				Refer = container,
				Message = input,
			};

			var result = await _gpt.GetAnser(send);

			return result;
		}
	}
}