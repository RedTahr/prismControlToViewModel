using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using prismControlToViewModel.Models;
using System.Collections.ObjectModel;

namespace prismControlToViewModel.ViewModels {
	public class MainPageViewModel : BindableBase, INavigationAware {
		private string _title;
		public string Title {
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		private List<Thing> boundThings = new List<Thing>();
		public List<Thing> BoundThings {
			get { return boundThings; }
			set { SetProperty(ref boundThings, value); }
		}

		public MainPageViewModel() {

		}

		public void OnNavigatedFrom(NavigationParameters parameters) {
		}

		public void OnNavigatingTo(NavigationParameters parameters) {
			var things = new List<Thing> {
				new Thing { Title = "title one - about", Detail = "one detail", Image = "icon.png", Url = "prismControlToViewModel:///NavigationPage/MainPage/AboutPage" },
				new Thing { Title = "two titles - next", Detail = "the quick brown fox jumped over the lazy dog detail", Image = "icon.png", Url = "prismControlToViewModel:///NavigationPage/MainPage/NextPage" },
				new Thing { Title = "gamma title - home", Detail = "greek detail", Image = "icon.png", Url = "prismControlToViewModel:///NavigationPage/MainPage/AboutPage/MainPage/NextPage/MainPage" }
			};
			BoundThings = things;
		}

		public void OnNavigatedTo(NavigationParameters parameters) {
			if (parameters.ContainsKey("title")) {
				Title = (string)parameters["title"] + " and Prism";
			}
		}
	}
}
