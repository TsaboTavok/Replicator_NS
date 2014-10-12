using System.Windows;

namespace Replicator_NS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var x = ((MainWindowVeiwModel) DataContext).ReplicationObject;
        }
    }
}
