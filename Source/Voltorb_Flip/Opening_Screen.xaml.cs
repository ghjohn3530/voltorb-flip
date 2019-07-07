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
using System.Windows.Navigation;

namespace Voltorb_Flip
{
    /// <summary>
    /// Interaction logic for Opening_Screen.xaml
    /// </summary>
    public partial class Opening_Screen : Window
    {
        public Opening_Screen()
        {
            InitializeComponent();
            Show();
            if (Text.loads.Length <= 0)
            {
                B_Load.IsEnabled = false;
            }
        }

        private void B_New_Click(object sender, RoutedEventArgs e)
        {
            Text.NewSave = true;
            Game game_screen = new Game();
            Close();
        }

        private void B_Load_Click(object sender, RoutedEventArgs e)
        {
            LoadFile load_screen = new LoadFile();
            Text.NewSave = false;
            Close();
        }
    }
}
