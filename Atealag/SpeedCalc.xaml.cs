using System.Windows;

namespace Atealag
{
    /// <summary>
    /// Interaction logic for SpeedCalc.xaml
    /// </summary>
    public partial class SpeedCalc : Window
    {
        SpeedBox globSpBox;
        public SpeedCalc(SpeedBox box)
        {
            DataContext = box;
            globSpBox = box;
            InitializeComponent();
        }
    }
}
