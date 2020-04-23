using Microsoft.Win32;
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
    /// Interaction logic for CharacterSheet.xaml
    /// </summary>
    public partial class CharacterSheet : Window
    {
        Sheet charSheet;
        public CharacterSheet()
        {
            InitializeComponent();
            charSheet = new Sheet();
            AllTabsGrid.DataContext = charSheet.userCharVals;
            MainTabGrid.DataContext = charSheet.userMainTab;
            AbilityScoreGrid.DataContext = charSheet.userCharVals;
            HPDisplayGrid.DataContext = charSheet.userMainTab.hpDisplay;
            ACDisplayGrid.DataContext = charSheet.userMainTab.acDisplay;
            SpeedDisplayGrid.DataContext = charSheet.userMainTab.speedDisplay;
            SavingThrowsGrid.DataContext = charSheet.userMainTab.savingThrowsDisplay;
            InitGrid.DataContext = charSheet.userMainTab.initCalcDisplay;
            ProficientBonusGrid.DataContext = charSheet.userCharVals;
        }
        public CharacterSheet(string s)
        {
            InitializeComponent();
            charSheet = new Sheet(s);

            AllTabsGrid.DataContext = charSheet.userCharVals;
            MainTabGrid.DataContext = charSheet.userMainTab;
            AbilityScoreGrid.DataContext = charSheet.userCharVals;
            HPDisplayGrid.DataContext = charSheet.userMainTab.hpDisplay;
            ACDisplayGrid.DataContext = charSheet.userMainTab.acDisplay;
            SpeedDisplayGrid.DataContext = charSheet.userMainTab.speedDisplay;
            SavingThrowsGrid.DataContext = charSheet.userMainTab.savingThrowsDisplay;
            InitGrid.DataContext = charSheet.userMainTab.initCalcDisplay;
            ProficientBonusGrid.DataContext = charSheet.userCharVals;
        }
        private void OpenHPCalc_Click(object sender, RoutedEventArgs e)
        {
            //Create a paramterized constructor for this to display values dynamically.
            HPCalc hpCalcWindow = new HPCalc(charSheet.userMainTab.hpDisplay);
            hpCalcWindow.Show();
        }

        private void OpenACCalc_Click(object sender, RoutedEventArgs e)
        {
            //Create a paramterized constructor for this to display values dynamically.
            ACCalcWindow acCalcWindow = new ACCalcWindow(charSheet.userMainTab.acDisplay);
            acCalcWindow.Show();
        }

        private void OpenSpeedCalc_Click(object sender, RoutedEventArgs e)
        {
            SpeedCalc speedCalcWindow = new SpeedCalc(charSheet.userMainTab.speedDisplay);
            speedCalcWindow.Show();
        }

        private void OpenSavingThrowsCalc_Click(object sender, RoutedEventArgs e)
        {
            SavingThrowsCalcWindow savingThrowsWindow = new SavingThrowsCalcWindow(charSheet.userMainTab.savingThrowsDisplay);
            savingThrowsWindow.Show();
        }

        private void InitCalc_Click(object sender, RoutedEventArgs e)
        {
            InitCalcWindow initCalcWindow = new InitCalcWindow(charSheet.userMainTab.initCalcDisplay);
            initCalcWindow.Show();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(charSheet.fileName == null)
            {
                SaveAsBtn_Click(this, e);
                return;
            }
            charSheet.saveSheet(charSheet.fileName);
        }

        private void SaveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                charSheet.saveAsSheet(openFileDialog.FileName);
            }
        }
    }
}
