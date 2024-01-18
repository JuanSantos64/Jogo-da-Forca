using AppJogoForca.Libraries.Text;
using AppJogoForca.Models;
using AppJogoForca.Repositories;
using System.Text;

namespace AppJogoForca
{
    public partial class MainPage : ContentPage
    {
        private Word _word;
        private int _errors;

        public MainPage()
        {
            InitializeComponent();
            ResetScreen();
            
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            String letter = button.Text;

            var positions = _word.Text.GetPositions(letter);

            if (positions.Count == 0)
            {
                ErrorHandler(button);
                await IsGameOver();
                return;
            }

            ReplaceLetter(letter, positions);
            button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Sucess"] as Style;

            HasWinner();

        }
        private void OnButtonClickedResetGame(object sender, EventArgs e)
        {
            ResetScreen();

        }
        #region Handler Sucess
        private async Task HasWinner()
        {
            if (!lblText.Text.Contains("_"))
            {
                await DisplayAlert("Ganhou!", "Você ganhou o Jogo", "Novo Jogo");
                ResetScreen();
            }
        }



        private void ReplaceLetter(string letter, List<int> positions)
        {
            foreach (int position in positions)
            {
                lblText.Text = lblText.Text.Remove(position, 1).Insert(position, letter);

            }
        }
        #endregion
        #region Handler Failed
        private void ErrorHandler(Button button)
        {
            _errors++;
            ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
            button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Failed"] as Style;
        }

        private async Task IsGameOver()
        {
            if (_errors == 6)
            {
                await DisplayAlert("Perdeu!", "Você foi enforcado", "Novo Jogo");
                ResetScreen();
            }
        }
        #endregion
        #region Reset Screen - Back Screen to Initial State
        private void ResetScreen()
        {
            ResetErrors();
            ResetVirtualKeyboard();
            GenerateWord();
            
        }
        private void GenerateWord()
        {
            var repository = new WordRepositories();
            _word = repository.GetRandomWord();

            lblTips.Text = _word.Tips;
            lblText.Text = new string('_', _word.Text.Length);
        }
        private void ResetErrors()
        {
            _errors = 0;
            ImgMain.Source = ImageSource.FromFile("forca1.png");
        }

        private void ResetVirtualKeyboard()
        {
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[0]);
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[1]);
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[2]);
            
        }

        private void ResetVirtualLines(HorizontalStackLayout horizontal)
        {
            foreach(Button button in horizontal.Children)
            {
                button.IsEnabled = true;
                button.Style = null;
            }
        }
        #endregion
    }

}
