using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace MauiApp1;

public partial class SecondPage : ContentPage
{
	public ObservableCollection<string> Items { get; set; }

	private SecondPageViewModel ViewModel;
	public SecondPage(ObservableCollection<string> items)
	{
		InitializeComponent();
		ViewModel = new SecondPageViewModel(items);
        BindingContext = ViewModel;

		ViewModel.Items.CollectionChanged += Items_CollectionChanged;
	
    }

	private void Items_CollectionChanged(object sende, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
	{
		SaveShoppingList();
	}

	private void SaveShoppingList()
	{
		string json = JsonSerializer.Serialize(ViewModel.Items);
		Preferences.Set("ShoppingList", json);

	}

	private async void OnGoBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private void OnClickedEdit(object sender, EventArgs e)
	{

		ViewModel.IsReadOnly = !ViewModel.IsReadOnly;
		ViewModel.EditButtonText = ViewModel.IsReadOnly ? "Редактировать" : "Сохранить";

    }

	private void OnCLickedDelete(object sender, EventArgs e)
	{
		ViewModel.Items.Clear();

		SaveShoppingList();
	}
}