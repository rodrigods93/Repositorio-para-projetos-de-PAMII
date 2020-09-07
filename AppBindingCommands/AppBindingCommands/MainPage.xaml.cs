using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBindingCommands
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "Info", async (msg) =>
                {
                    await DisplayAlert("Informação", msg, "OK");
                });

            MessagingCenter.Subscribe<string>(this, "Pergunta", async (msg) =>
                {


                    if (await DisplayAlert("Confirmação",
                        $"{msg} , confirma limpeza dos dados?", "Yes", "No"))
                    {
                        viewModel.CleanFields();
                        await DisplayAlert("Informação", "Limpeza realizada com sucesso", "Ok");
                    }


                });

            MessagingCenter.Subscribe<string>(this, "Opcoes", async (msg) =>
            {
                string result;

                result = await DisplayActionSheet($"{msg}, selecione uma opção: ",
                    "Cancelar", "Limpar", "Contar Caracteres", "Exibir Saudação");

                if (result != null)
                {
                    if (result.Equals("Limpar"))
                        viewModel.CleanConfirmation();

                    if (result.Equals("Contar Caracteres"))
                        viewModel.CountCharacters();

                    if (result.Equals("Exibir Saudação"))
                        viewModel.ShowMessage();
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "Info");
            MessagingCenter.Unsubscribe<string>(this, "Pergunta");
            MessagingCenter.Unsubscribe<string>(this, "Opcoes");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string informacoes = string.Empty;

            if (Application.Current.Properties.ContainsKey("AcaoInicial"))
                informacoes += Application.Current.Properties["AcaoInicial"];

            if (Application.Current.Properties.ContainsKey("AcaoStart"))
                informacoes += Application.Current.Properties["AcaoStart"];

            if (Application.Current.Properties.ContainsKey("AcaoSleep"))
                informacoes += Application.Current.Properties["AcaoSleep"];

            if (Application.Current.Properties.ContainsKey("AcaoResume"))
                informacoes += Application.Current.Properties["AcaoResume"];

            lblInformacoes.Text = informacoes;


        }
    }
}
