using System;
using System.Windows;
using System.Windows.Controls;

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
            for (var i = 0; i < diceNumber; i++)
            {
                rndDMG = rnd.Next(1, 10);
                rndDMG2 = rndDMG + rndDMG2;
            }
            Result.Text = rndDMG2.ToString();
        }
    }
}
