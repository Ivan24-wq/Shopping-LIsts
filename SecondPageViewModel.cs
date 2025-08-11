using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1
{
    public class SecondPageViewModel : INotifyPropertyChanged
    {
        private bool _isReadOnly = true;
        private string _editButtonText = "Редактировать";

        public ObservableCollection<string> Items { get; set; }

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }

        public string EditButtonText
        {
            get => _editButtonText;
            set
            {
                _editButtonText = value;
                OnPropertyChanged();
            }
        }

        public SecondPageViewModel(ObservableCollection<string> items)
        {
            Items = items;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
