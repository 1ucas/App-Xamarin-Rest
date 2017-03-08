using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cats
{
	public partial class CatsPage : ContentPage
	{
		public CatsPage()
		{
			InitializeComponent();
			this.BindingContext = new CatsViewModel();
			ListViewCats.ItemSelected += ListViewCats_ItemSelected;
		}

		private async void ListViewCats_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var SelectedCat = e.SelectedItem as Cat;
			if (SelectedCat != null)
			{
				await Navigation.PushAsync(new DetailsPage(SelectedCat));
				ListViewCats.SelectedItem = null;
			}
		}


	}
}
