using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			BindableProperty.Create(nameof(Things), typeof(IList), typeof(Thing), null, propertyChanged: OnThingsChanged);

		public IReadOnlyList<Thing> Things {
			get { return (IReadOnlyList<Thing>)base.GetValue(ThingsProperty); }
			set { base.SetValue(ThingsProperty, value); }
		}

		public ControlThing() {
			Content = stack;
		}
		protected override void OnBindingContextChanged() {
			base.OnBindingContextChanged();
			RemoveBinding(ThingsProperty);
			SetBinding(ThingsProperty, new Binding(nameof(Things)));
		}

		private static void OnThingsChanged(BindableObject bindable, object oldValue, object newValue) {
			if (oldValue == newValue) { return; }
			var items = (IList<Thing>)newValue;
			if (items == null) { return; }
			stack.Children.Clear();
			if (items.Count == 0) {
				return;
			}
			
			foreach (var item in items) {
				if (item == null) {
					continue;
				}
				var layout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
					Padding = new Thickness(0, 4, 0, 4),
					Children = {
						new Image { Source = item.Image },
						new Label { Text = item.Title },
					}
				};

				layout.BindingContext = item;
				stack.Children.Add(layout);
			}
		}

	}
}
