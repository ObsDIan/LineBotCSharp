using Microsoft.Identity.Client;
using System.Drawing.Printing;

namespace AskApi.ViewModels
{
	public class MSViewModel
	{
		public List<message> messages { get; set; }
	}

	public class message
	{
		public string role { get; set; }
		public string content { get; set; }
	}


}
