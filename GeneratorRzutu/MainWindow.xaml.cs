using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace GeneratorRzutu

{
    public partial class MainWindow
    {
        string currentExePath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Title = "Okno";
            Loaded += PageLoaded;
        }
        private void DiceRoll_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            var diceNumber = DiceNumber.Text == "" ? 0 : int.Parse(DiceNumber.Text); //ilość kości
            var diceDim = DiceDimension.Text == "" ? 0 : int.Parse(DiceDimension.Text); //typ kości (ile ścian posiada kość)      
            var multiply = Multiplier.Text == "" ? 0 : int.Parse(Multiplier.Text); //mnożnik rzutu
            var parameter = Parameter.Text == "" ? 0 : int.Parse(Parameter.Text); //współczynnik dodawany do sumy
            var newLine = Environment.NewLine; //nowa linia w textBlock
            var nums = rnd.Next(1, diceDim + 1);
            var sum = nums;
            if (TextBlock != null)
            {
                TextBlock.Text = nums.ToString();
                for (var i = 1; i < diceNumber; i++)
                {
                    nums = rnd.Next(1, diceDim + 1);
                    TextBlock.Text = TextBlock.Text + newLine + nums;
                    sum = nums + sum;
                }
            }
            ThrowSum.Text = (sum * multiply + parameter).ToString();
            if (customDiceCheckbox.IsChecked != null && customDiceCheckbox.IsChecked.Value)
            {
                var startOfSuccessDice = startSuccess.Text == "" ? 0 : int.Parse(startSuccess.Text);
                var endOfSuccessDice = endSuccesss.Text == "" ? 0 : int.Parse(endSuccesss.Text);
                var startOfNoSuccessDice = startNoSuccess.Text == "" ? 0 : int.Parse(startNoSuccess.Text);
                var endOfNoSuccessDice = endNoSuccess.Text == "" ? 0 : int.Parse(endNoSuccess.Text);
                var startOfFailSuccessDice = startFailSuccess.Text == "" ? 0 : int.Parse(startFailSuccess.Text); 
                var endOfFailSuccessDice = endFailSuccess.Text == "" ? 0 : int.Parse(endFailSuccess.Text);
                var randomCustomDice = rnd.Next(startOfSuccessDice, endOfFailSuccessDice);
                ThrowSum.Text = 0.ToString();
                if (randomCustomDice>=startOfSuccessDice && randomCustomDice<=endOfSuccessDice)
                {
                    TextBlock.Text = nameSuccess.Text+newLine+randomCustomDice.ToString();
                }
                if (randomCustomDice >= startOfNoSuccessDice && randomCustomDice <= endOfNoSuccessDice)
                {
                    TextBlock.Text = nameNoSuccess.Text + newLine + randomCustomDice.ToString();
                }
                if (randomCustomDice >= startOfFailSuccessDice && randomCustomDice <= endOfFailSuccessDice)
                {
                    TextBlock.Text = nameFailSuccess.Text+newLine+randomCustomDice.ToString();
                }
            }
        }
        private void DiceNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char) KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Tab &
                e.Key != Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void DiceDimension_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char) KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Tab &
                e.Key != Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void Multiplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char) KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Tab &
                e.Key != Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void Parameter_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char) KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.OemMinus &
                e.Key != Key.Tab & e.Key != Key.Enter) //blokuje wszystkie znaki oprócz Backspace i "minusa"
            {
                e.Handled = true;
            }
        }

        private void DiceNumber_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox) sender).SelectAll();
        }

        private void DiceDimension_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox) sender).SelectAll();
        }

        private void Multiplier_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox) sender).SelectAll();
        }

        private void Parameter_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox) sender).SelectAll();
        }

        private void WindowName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Title = WindowName.Text;
        }

        private void WindowName_GotFocus(object sender, RoutedEventArgs e)
        {
            var wn = (TextBox) sender;
            wn.Text = string.Empty;
            wn.GotFocus -= WindowName_GotFocus;
        }

        private void WindowName_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox) sender).SelectAll();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var savedData = new string[4]; //4 elementowa tablica string przechowujaca wartosci do zapisania
            savedData[0] = DiceNumber.Text;
            savedData[1] = DiceDimension.Text;
            savedData[2] = Parameter.Text;
            savedData[3] = Multiplier.Text;
            var saveFileDialog = new SaveFileDialog {Filter = "Plik tekstowy (*.txt)|*.txt"};
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllLines(saveFileDialog.FileName, savedData);
            }
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Plik tekstowy (*.txt)|*.txt"};
            if (openFileDialog.ShowDialog() == true)
            {
                var loadedData = File.ReadAllLines(openFileDialog.FileName); //tablica przechowujaca wczytane wartosci
                DiceNumber.Text = loadedData.ElementAtOrDefault(0); //wczytanie pierwszego elementu itd.
                DiceDimension.Text = loadedData.ElementAtOrDefault(1);
                Parameter.Text = loadedData.ElementAtOrDefault(2);
                Multiplier.Text = loadedData.ElementAtOrDefault(3);
            }
        }

        private void Wh40K_OnClick(object sender, RoutedEventArgs e)
        {
            var wh40K = new Wh40KWindow();
            wh40K.Show();
        }
        private void Profiler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFile = Profiler.SelectedItem.ToString();
            string filex = $@"{currentExePath}\{selectedFile}.txt";
            string[] loadedData = File.ReadAllLines(filex);
            DiceNumber.Text = loadedData.ElementAtOrDefault(0);
            DiceDimension.Text = loadedData.ElementAtOrDefault(1);
            Parameter.Text = loadedData.ElementAtOrDefault(2);
            Multiplier.Text = loadedData.ElementAtOrDefault(3);
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dinfo = new DirectoryInfo($@"{currentExePath}");
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                Profiler.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }           
        }
    }
}