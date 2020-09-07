using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppBindingCommands
{
    class MainPageViewModel : INotifyPropertyChanged

    {
        public ICommand ShowMessageCommand { get; }
        public ICommand CountCommand { get; }

        public ICommand CleanCommand { get; }

        public ICommand OptionCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        string name = string.Empty;

        public string Name
        {
            get => name;
            set
            {
                if (name == null)
                    return;

                name = value;
                OnPropertyChanged(nameof(name));
                OnPropertyChanged(nameof(DisplayName));
            }
        }
        public string DisplayName => $"Nome digitado : {name}";



        string displayMessage = string.Empty;
        public string DisplayMessage
        {
            get => displayMessage;
            set
            {

                if (displayMessage == null)
                    return;

                displayMessage = value;
                OnPropertyChanged(nameof(DisplayMessage));

            }
        }

        public MainPageViewModel()
        {
            ShowMessageCommand = new Command(ShowMessage);
            CountCommand = new Command(CountCharacters);
            CleanCommand = new Command(CleanConfirmation);
            OptionCommand = new Command(ShowOptions);
        }
        public void ShowMessage()
        {
            string dataProperty = Application.Current.Properties["dtAtual"].ToString();

            DisplayMessage = $"Boa noite {Name}. Hoje é {dataProperty}.";

        }

        public void CountCharacters()
        {
            string nameLenght = string.Format("Seu nome tem {0} Letras", name.Length);
            MessagingCenter.Send<string>(nameLenght, "Info");
        }

        public void CleanConfirmation()
        {
            MessagingCenter.Send<string>(Name, "Pergunta");
        }

        public void CleanFields()
        {
            Name = string.Empty;
            DisplayMessage = string.Empty;
            OnPropertyChanged(Name);
            OnPropertyChanged(DisplayMessage);
        }

        public void ShowOptions()
        {
            MessagingCenter.Send <string>(Name, "Opcoes");
        }
    }
}
