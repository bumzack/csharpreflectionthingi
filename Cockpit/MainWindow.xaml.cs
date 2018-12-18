using DaoThingi.Reflection;
using DaoThingi.TestDI;
using System.Windows;

namespace Cockpit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Autowire]
        IWetrManager wetrManager;
        
        public MainWindow()
        {
            InitializeComponent();

            wetrManager.SayHello();
        }
    }
}
