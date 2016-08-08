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
                if (Tearing.IsChecked != null && Tearing.IsChecked.Value)
                {
                    rndTearing = rnd.Next(1, 11);
                    var leastValue = Math.Min(rndTearing, Math.Min(rndDMG, rndDMG2));
                    rndDMG2 = rndDMG2 - leastValue;
                }
                if (Proven.IsChecked != null && Proven.IsChecked.Value)
                {
                    var chosenValue = ProvenTextbox.Text == "" ? 0 : int.Parse(ProvenTextbox.Text);
                    if (chosenValue < rndDMG || chosenValue < rndDMG2)
                    {
                        rndDMG = (chosenValue < rndDMG) ? rnd.Next(1, 11) : rndDMG;
                        rndDMG2 = (chosenValue < rndDMG2) ? rnd.Next(1, 11) : rndDMG2;
                    }                   
                }
                if (Toxic.IsChecked != null && Toxic.IsChecked.Value)
                {
                    
                }
                if (Volitile.IsChecked != null && Volitile.IsChecked.Value)
                {
                    if (rndDMG == 9 && rndDMG == 10)
                    {
                        rndDMG2 = rnd.Next(1, 11);
                    }
                }                          
            }
            var modifier = int.Parse(Modifier.Text);            
            rndDMG2 = rndDMG + rndDMG2 + rndTearing + modifier;
            Result.Text ="Wynik rzutu: " + rndDMG2;
        }
        private void DiceNumber_KeyDown(object sender, KeyEventArgs e)
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
        private void Modificator_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back & e.Key != Key.Tab &
               e.Key != Key.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
