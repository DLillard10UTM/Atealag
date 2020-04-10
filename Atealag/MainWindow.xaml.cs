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
            DataContext = userAssetManager;
        }
        void HPTrackerAdd_Click(Object sender, EventArgs e)
        {
            userAssetManager.userHPTrack.AddFromButton();
        }
    }
}
