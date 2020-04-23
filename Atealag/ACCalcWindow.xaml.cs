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
    /// Interaction logic for ACCalcWindow.xaml
    /// </summary>
    public partial class ACCalcWindow : Window
    {
        ReadOnlyCollection<string> abilityScores { get; } = new ReadOnlyCollection<string>(
        new string[] { "Strength", "Intelligence", "Dexterity", "Wisdom", "Constitution", "Charisma" });
        ACBox globacBox;
        public ACCalcWindow(ACBox acBox)
        {
            DataContext = acBox;
            globacBox = acBox;
            InitializeComponent();
            for (int i = 0; i < abilityScores.Count; i++)
            {
                PrimaryAbilityScoreDropDown.Items.Add(abilityScores[i]);
                SecondaryAbilityScoreDropDown.Items.Add(abilityScores[i]);
            }
            SecondaryAbilityScoreDropDown.Items.Add("N/A");
            PrimaryAbilityScoreDropDown.SelectedIndex = acBox.primarySubCouple.getScoreIndex();
            PrimaryAbilityScoreDropDown.SelectionChanged += new SelectionChangedEventHandler(PrimaryAbilityScoreDropDown_SelectionChanged);
            
            
            SecondaryAbilityScoreDropDown.SelectedIndex = acBox.secondarySubCouple.getScoreIndex();
            SecondaryAbilityScoreDropDown.SelectionChanged += new SelectionChangedEventHandler(SecondaryAbilityScoreDropDown_SelectionChanged);
        }

        private void PrimaryAbilityScoreDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            globacBox.primarySubCouple.changeAbScore(globacBox.primarySubCouple.getScoreIndex(), PrimaryAbilityScoreDropDown.SelectedIndex);
        }

        private void SecondaryAbilityScoreDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            globacBox.secondarySubCouple.changeAbScore(globacBox.secondarySubCouple.getScoreIndex(), SecondaryAbilityScoreDropDown.SelectedIndex);
        }
    }
}
