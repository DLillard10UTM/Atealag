using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HPCalc.xaml
    /// </summary>
    public partial class HPCalc : Window
    {
        ReadOnlyCollection<string> abilityScores { get; } = new ReadOnlyCollection<string>(
        new string[] { "Strength", "Intelligence", "Dexterity", "Wisdom", "Constitution", "Charisma" });
        HealthBox globhpBox;
        //Non param for "new Sheet"
        public HPCalc(HealthBox hpBox)
        {
            DataContext = hpBox;
            globhpBox = hpBox;
            InitializeComponent();
            for(int i = 0; i < abilityScores.Count; i++)
            {
                AbilityScoreDropDown.Items.Add(abilityScores[i]);
            }
            //because we have to reload the page each time, we have to set the selected index each time.
            AbilityScoreDropDown.SelectedIndex = hpBox.HPCalcCouple.getScoreIndex();

            AbilityScoreDropDown.SelectionChanged += new SelectionChangedEventHandler(AbilityScoreDropDownSelectionChanged);
        }
        void AbilityScoreDropDownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            globhpBox.HPCalcCouple.changeAbScore(globhpBox.HPCalcCouple.getScoreIndex(), AbilityScoreDropDown.SelectedIndex);
        }
    }
}
