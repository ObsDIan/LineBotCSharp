using AskApi.Extensions;
using AskApi.ViewModels;
using HKDB.Models;

namespace AskApi.Services
{
	public class SimService
	{
		public async Task<List<SimData>> SimMethod(float[] input, IEnumerable<Embedding> embeddings)
		{
			var SimDatas = new List<SimData>();
			//var embeddings = T.ToList();

			//平行處理
			await Task.Run(() =>
			{
				foreach (var item in embeddings)
				{
					var sim = input.CosineSimilarity(VStr.StrToVec(item.EmbeddingVector));

					if (sim > 0.8)
					{
						var SimData = new SimData()
						{
							MessageEmbedding = input,
							DbEmbedding = item.EmbeddingVector,
							DbId = item.EmbeddingId,
							Similarity = sim
						};
						SimDatas.Add(SimData);
					}
				}
			});

			return SimDatas;
		}
	}
}
