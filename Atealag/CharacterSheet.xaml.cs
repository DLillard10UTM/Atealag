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
            MainTab.DataContext = charSheet.userMainTab;
            AbilityScoreGrid.DataContext = charSheet.userCharVals;
        }
    }
}
