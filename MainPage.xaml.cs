using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Collections.Specialized;
using System.Text.Json;
namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<string> shoppingLIst = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();

            LoadShoppingList();
            shoppingLIst.CollectionChanged += ShoppingList_CollectionChanged;
            BindingContext = this;
         
        }

        private void ShoppingList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SaveShoppingList();
        }

        private void SaveShoppingList()
        {
            string json = JsonSerializer.Serialize(shoppingLIst);
            Preferences.Set("ShoppingList", json);
        }
        private void LoadShoppingList()
        {
            string json = Preferences.Get("ShoppingList", string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                var items = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
                if(items != null)
                {
                    shoppingLIst = items;
                    shoppingLIst.CollectionChanged += ShoppingList_CollectionChanged;
                }
            }
        }

        private async void OnGoToTrash(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage(shoppingLIst));
        }

        private void OnCliccked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(search.Text))
            {
                shoppingLIst.Add(search.Text.Trim());
                search.Text = string.Empty;
            }
        }
        private async void OnSettingClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Сменить тему", "Отмена", null, "Светлая", "Тёмная");
            switch (action)
            {
                case "Светлая":
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;

                case "Тёмная":
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;
            }
        }
    }

   
}
