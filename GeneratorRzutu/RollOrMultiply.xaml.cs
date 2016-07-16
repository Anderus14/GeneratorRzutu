using System;
using System.Windows;
using System.Windows.Input;

namespace GeneratorRzutu.Windows
{
    public partial class RollOrMultiply
    {
        public RollOrMultiply()
        {
            InitializeComponent();
        }
        private void DMGRoll_Click(object sender, RoutedEventArgs e)
        {                    
            var rnd = new Random();
            var rndDMG = 0;
            var diceNumber = int.Parse(DiceNumber.Text);
            var rndDMG2 = rndDMG;
            var rndTearing = 0;                     
            for (var i = 0; i < diceNumber; i++)
            {
                rndDMG = rnd.Next(1, 11);                
                if (rndDMG == 10)
                {
                    rndDMG2 = rnd.Next(1 ,11);
                }
                if (Tearing.IsChecked.Value)
                {
                    rndTearing = rnd.Next(1, 11);
                }
                if (Proven.IsChecked.Value)
                {
                    var ChosenValue = int.Parse(ProvenTextbox.Text);
                    
                }                             
            }
            var modifier = int.Parse(Modifier.Text);
            var leastValue = Math.Min(rndTearing, Math.Min(rndDMG, rndDMG2));
            rndDMG2 = rndDMG + rndDMG2 + rndTearing - leastValue + modifier;
            Result.Text = rndDMG2.ToString();
        }

        private void DiceNumber_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Tab &
                e.Key != Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void ProvenCheckbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Tab &
                e.Key != Key.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
