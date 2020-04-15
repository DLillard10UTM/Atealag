using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Atealag
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AssetManager userAssetManager;
        public MainWindow()
        {
            InitializeComponent();
            userAssetManager = new AssetManager();
            HPTrackerGrid.DataContext = userAssetManager.userHPTrack;
            InitTrackerGrid.DataContext = userAssetManager.userInitTrack;

            updateOrdering();
        }
        void HPTrackerAdd_Click(Object sender, EventArgs e)
        {
            userAssetManager.userHPTrack.AddFromButton();
        }
        private void InitTrackerAdd_Click(object sender, RoutedEventArgs e)
        {
            userAssetManager.userInitTrack.AddFromButton();
            updateOrdering();
        }

        private void HPTrackerDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext;
            int index = HPList.Items.IndexOf(item);
            HPList.SelectedItem = HPList.Items[index];
            userAssetManager.userHPTrack.RemoveFromButton(HPList.SelectedIndex);
        }
        private void InitTrackerDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext;
            int index = InitList.Items.IndexOf(item);
            InitList.SelectedItem = InitList.Items[index];
            userAssetManager.userInitTrack.RemoveFromButton(InitList.SelectedIndex);
        }

        private void InitTrackerUpdate_Click(object sender, RoutedEventArgs e)
        {
            updateOrdering();
        }

        private void updateOrdering()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(userAssetManager.userInitTrack.initBubbles);
            view.SortDescriptions.Add(new SortDescription("init", ListSortDirection.Ascending));
        }

        private void OpenSheetBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make it such that when the user opens the file it goes near the .exe for atealag.
			OpenFileDialog openFileDialog = new OpenFileDialog();
            _ = openFileDialog.ShowDialog() == true;
        }

        private void NewSheetBtn_Click(object sender, RoutedEventArgs e)
        {
            CharacterSheet newCharSheet = new CharacterSheet();
            newCharSheet.Show();
        }
    }
}
