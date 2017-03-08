using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cats
{
	public class CatsViewModel : INotifyPropertyChanged
	{
		public bool Busy;

		public bool IsBusy
		{
			get
			{
				return Busy;
			}
			set
			{
				Busy = value; 
				OnPropertyChanged();
				GetCatsCommand.ChangeCanExecute();
			}
		}

		public ObservableCollection<Cat> Cats { get; set; }

		public CatsViewModel()
		{
			Cats = new ObservableCollection<Cat>();

            GetCatsCommand = new Command(
				async () => await GetCats(),
				() => !IsBusy
				);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) =>
				PropertyChanged?.Invoke(this,
					new PropertyChangedEventArgs(propertyName));

		async Task GetCats()
		{
			if (!IsBusy)
			{
				try
				{
					IsBusy = true;
					var Repository = new Repository();
					Cats.Clear();
					var Items = await Repository.GetCats();
					foreach (var Cat in Items)
					{
						Cats.Add(Cat);
					}
				}
				catch (Exception ex)
				{
					await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
				"Error!", ex.Message, "OK");
				}
				finally
				{
					IsBusy = false;
				}
			}
			return;
		}

		public Command GetCatsCommand { get; set; }
	}
}
