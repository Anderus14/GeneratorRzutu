using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GeneratorRzutu
{
    public partial class Wh40KWindow
    {
        public Wh40KWindow()
        {
            InitializeComponent();
        }
        private void Roll_Click(object sender, RoutedEventArgs e)
        {           
            var k100 = K100.Text == "" ? 0 : int.Parse(K100.Text);
            var wh40k_rnd = new Random();
            var number=wh40k_rnd.Next(1, 101);
            var newline = Environment.NewLine;
            if (k100 >= number)
            {
                var result = k100 - number;
                var restFor10 = number % 10;
                var rest10 = 10 - restFor10;
                var finalResult10 = (result + rest10) / 10;
                var restFor20 = number % 10;
                var rest20 = 10 - restFor20;
                var finalResult20 = (result + rest20) / 20;
                if (Without.IsChecked != null && Without.IsChecked.Value)
                {
                    NumberOfSuccesses.Text = "1 sukces" + newline + "Twój rzut to " + number;
                }                                
                if (Every10.IsChecked != null && Every10.IsChecked.Value && finalResult10 != 0)
                {
                    if (finalResult10 >= 5)
                    {
                        NumberOfSuccesses.Text = (finalResult10).ToString() + " sukcesów" + newline + "Twój rzut to " + number;
                    }
                    else if (finalResult10 == 1)
                    {
                        NumberOfSuccesses.Text = "1 sukces" + newline + "Twój rzut to " + number;
                    }
                    else
                    {
                        NumberOfSuccesses.Text = (finalResult10).ToString() + " sukcesy" + newline + "Twój rzut to " + number;
                    }                  
                }
                if (Every20.IsChecked != null && Every20.IsChecked.Value && finalResult20 != 0)
                {
                    if (finalResult20 >= 5)
                    {
                        NumberOfSuccesses.Text = (finalResult20).ToString() + " sukcesów" + newline + "Twój rzut to " + number;
                    }
                    else if (finalResult20 == 1)
                    {
                        NumberOfSuccesses.Text ="1 sukces" + newline + "Twój rzut to " + number;
                    }
                    else
                    {
                        NumberOfSuccesses.Text = (finalResult20).ToString() + " sukcesy" + newline + "Twój rzut to " + number;
                    }
                }
            }
            else
            {
                NumberOfSuccesses.Text = "Brak sukcesów" + newline + "Twój rzut to " + number;
            }
        }

        private void K100_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Tab & e.Key != Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void K100_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox)sender).SelectAll();
        }
    }
}
