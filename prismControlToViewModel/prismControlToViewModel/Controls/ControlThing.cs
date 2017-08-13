using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using prismControlToViewModel.Models;
using Xamarin.Forms;

namespace prismControlToViewModel.Controls {
	public class ControlThing : ContentView {
		static StackLayout stack = new StackLayout {
			Padding = new Thickness(0, 4, 0, 4),
			Children = {
				new Label { Text = nameof(Things) }
				}
		};

		public static readonly BindableProperty ThingsProperty =
			BindableProperty.Create(nameof(Things), typeof(IList<Thing>), typeof(ControlThing), null);//, propertyChanging: Changing, propertyChanged: Changed);

		public IList<Thing> Things {
			get { return (IList<Thing>)base.GetValue(ThingsProperty); }
			set { base.SetValue(ThingsProperty, value); }
		}
		
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(ControlThing), default(Command), BindingMode.OneWay);

		//private static void Changed(BindableObject bindable, object oldValue, object newValue) {
		//	System.Diagnostics.Debug.WriteLine($"changed");
		//}

		//private static void Changing(BindableObject bindable, object oldValue, object newValue) {
		//	System.Diagnostics.Debug.WriteLine("changing");
		//}

		public Command Command {
			get { return (Command)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public ControlThing() {
			Content = stack;
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(Things) && Things != null && Things.Count > 0) {
				RebuildUI();
			}
		}

		protected override void OnPropertyChanging([CallerMemberName] string propertyName = null) {
			base.OnPropertyChanging(propertyName);
		}

		protected override void OnBindingContextChanged() {
			base.OnBindingContextChanged();
		}

		private void RebuildUI() {
			stack.Children.Clear();
			if (Things == null || Things.Count == 0) {
				return;
			}

			foreach (var item in Things) {
				if (item == null) {
					continue;
				}
				var layout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
					Padding = new Thickness(0, 4, 0, 4),
					Children = {
						new Image { Source = item.Image },
						new StackLayout {
						Orientation = StackOrientation.Vertical,
						Padding = 0,
						Children = {
							new Label { Text = item.Title },
							new Label { Text = item.Detail, FontSize = (double)NamedSize.Micro },
							}
						}
					}	
				};
				
				var tapGesture = new TapGestureRecognizer();
				tapGesture.Command = ItemTapped;
				tapGesture.CommandParameter = item.Url;
				layout.GestureRecognizers.Add(tapGesture);

				stack.Children.Add(layout);
			}
		}
		
		public Command ItemTapped {
			get {
				return new Command((obj) =>
				{
					var url = (int)obj;

					if (Command != null) {
						Command.Execute(url);
					}
				});
			}
		}

	}
}
