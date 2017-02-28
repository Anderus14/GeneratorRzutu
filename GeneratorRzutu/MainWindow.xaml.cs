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
                CheckBoxChecked(sender, e);
                CheckBoxUnchecked(sender, e);                               
        }
        private void Correct_KeyDown(object sender, KeyEventArgs e)
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
        public void CheckBoxChecked(object sender, EventArgs e)
        {
           try
            {
                if (customDiceCheckbox.IsChecked != null && customDiceCheckbox.IsChecked.Value)
                {
                    Random rnd = new Random();
                    var diceNumber = DiceNumber.Text == "" ? 0 : int.Parse(DiceNumber.Text); //ilość kości
                    var newLine = Environment.NewLine; //nowa linia w textBlock
                    var startOfSuccessDice = startSuccess.Text == "" ? 0 : int.Parse(startSuccess.Text);
                    var endOfSuccessDice = endSuccesss.Text == "" ? 0 : int.Parse(endSuccesss.Text);
                    var startOfNoSuccessDice = startNoSuccess.Text == "" ? 0 : int.Parse(startNoSuccess.Text);
                    var endOfNoSuccessDice = endNoSuccess.Text == "" ? 0 : int.Parse(endNoSuccess.Text);
                    var startOfFailSuccessDice = startFailSuccess.Text == "" ? 0 : int.Parse(startFailSuccess.Text);
                    var endOfFailSuccessDice = endFailSuccess.Text == "" ? 0 : int.Parse(endFailSuccess.Text);
                    var randomCustomDice = rnd.Next(startOfSuccessDice, endOfFailSuccessDice);
                    ThrowSum.Text = null;
                    if (TextBlock != null)
                    {
                        var text = "";
                        for (var i = 1; i <= diceNumber; i++)
                        {
                            randomCustomDice = rnd.Next(startOfSuccessDice, endOfFailSuccessDice);
                            if (randomCustomDice >= startOfSuccessDice && randomCustomDice <= endOfSuccessDice)
                            {
                                text = nameSuccess.Text;
                            }
                            if (randomCustomDice >= startOfNoSuccessDice && randomCustomDice <= endOfNoSuccessDice)
                            {
                                text = nameNoSuccess.Text;
                            }
                            if (randomCustomDice >= startOfFailSuccessDice && randomCustomDice <= endOfFailSuccessDice)
                            {
                                text = nameFailSuccess.Text;
                            }
                            TextBlock.Text += text + newLine;
                        }                        
                    }
                }
            }
            catch
            {
                if (nameSuccess.Text == "Wpisz nazwę" || nameSuccess == null || nameNoSuccess.Text == "Wpisz nazwę" ||
                        nameNoSuccess == null || nameFailSuccess.Text == "Wpisz nazwę" || nameFailSuccess == null)
                {
                    MessageBox.Show("Ty Luju! Wpisz tekst, który ma się wyświetlić!");
                }
            }
        }
         public void CheckBoxUnchecked(object sender, EventArgs e)
        {
            Random rnd = new Random();
            var diceNumber = DiceNumber.Text == "" ? 0 : int.Parse(DiceNumber.Text); //ilość kości
            var diceDim = DiceDimension.Text == "" ? 0 : int.Parse(DiceDimension.Text); //typ kości (ile ścian posiada kość)      
            var multiply = Multiplier.Text == "" ? 0 : int.Parse(Multiplier.Text); //mnożnik rzutu
            var parameter = Parameter.Text == "" ? 0 : int.Parse(Parameter.Text); //współczynnik dodawany do sumy
            var newLine = Environment.NewLine; //nowa linia w textBlock
            var nums = rnd.Next(1, diceDim);
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
        }
    }
}