using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Atealag
{
    /// <summary>
    /// Interaction logic for SavingThrowsCalcWindow.xaml
    /// </summary>
    public partial class SavingThrowsCalcWindow : Window
    {
        public SavingThrowsCalc ourStc;
        public SavingThrowsCalcWindow(SavingThrowsCalc stc)
        {
            this.DataContext = stc;
            ourStc = stc;
            InitializeComponent();
            CheckBoxes();
        }

        private void CheckBoxes()
        {
            strProf.IsChecked = ourStc.checkBoxes[0];
            intelProf.IsChecked = ourStc.checkBoxes[1];
            dexProf.IsChecked = ourStc.checkBoxes[2];
            wisProf.IsChecked = ourStc.checkBoxes[3];
            conProf.IsChecked = ourStc.checkBoxes[4];
            chaProf.IsChecked = ourStc.checkBoxes[5];
        }
        private void strMisc_TextChanged(object sender, TextChangedEventArgs e)
        {
            int discard;
            bool succ = Int32.TryParse(strMisc.Text, out discard);
            if (succ)
            {
                ourStc.miscBonuses[0] = discard;
                ourStc.calculateTotalScore(0);
            }
        }

        private void intelMisc_TextChanged(object sender, TextChangedEventArgs e)
        {
            int discard;
            bool succ = Int32.TryParse(intelMisc.Text, out discard);
            if (succ)
            {
                ourStc.miscBonuses[1] = discard;
                ourStc.calculateTotalScore(1);
            }
        }

        private void dexMisc_TextChanged(object sender, TextChangedEventArgs e)
        {
            int discard;
            bool succ = Int32.TryParse(dexMisc.Text, out discard);
            if (succ)
            {
                ourStc.miscBonuses[2] = discard;
                ourStc.calculateTotalScore(2);
            }
        }

        private void wisMisc_TextChanged(object sender, TextChangedEventArgs e)
        {
            int discard;
            bool succ = Int32.TryParse(wisMisc.Text, out discard);
            if (succ)
            {
                ourStc.miscBonuses[3] = discard;
                ourStc.calculateTotalScore(3);
            }
        }

        private void conMisc_TextChanged(object sender, TextChangedEventArgs e)
        {
            int discard;
            bool succ = Int32.TryParse(conMisc.Text, out discard);
            if (succ)
            {
                ourStc.miscBonuses[4] = discard;
                ourStc.calculateTotalScore(4);
            }
        }

        private void chaMisc_TextChanged(object sender, TextChangedEventArgs e)
        {
            int discard;
            bool succ = Int32.TryParse(chaMisc.Text, out discard);
            if (succ)
            {
                ourStc.miscBonuses[5] = discard;
                ourStc.calculateTotalScore(5);
            }
        }

        private void strProf_Click(object sender, RoutedEventArgs e)
        {
            ourStc.checkBoxes[0] = !ourStc.checkBoxes[0];
            ourStc.calculateTotalScore(0);
        }

        private void intelProf_Click(object sender, RoutedEventArgs e)
        {
            ourStc.checkBoxes[1] = !ourStc.checkBoxes[1];
            ourStc.calculateTotalScore(1);
        }

        private void dexProf_Click(object sender, RoutedEventArgs e)
        {
            ourStc.checkBoxes[2] = !ourStc.checkBoxes[2];
            ourStc.calculateTotalScore(2);
        }

        private void wisProf_Click(object sender, RoutedEventArgs e)
        {
            ourStc.checkBoxes[3] = !ourStc.checkBoxes[3];
            ourStc.calculateTotalScore(3);
        }

        private void conProf_Click(object sender, RoutedEventArgs e)
        {
            ourStc.checkBoxes[4] = !ourStc.checkBoxes[4];
            ourStc.calculateTotalScore(4);
        }

        private void chaProf_Click(object sender, RoutedEventArgs e)
        {
            ourStc.checkBoxes[5] = !ourStc.checkBoxes[5];
            ourStc.calculateTotalScore(5);
        }
    }
}
