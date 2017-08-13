using MvvmHelpers;

namespace prismControlToViewModel.Models {
	public class Thing : IRelatedThing {
		
		public string Image { get; set; } = "";
		public string Title { get; set; }  = "";
		public string Detail { get; set; } = "";
		public string Url { get; set; }
	}
}
