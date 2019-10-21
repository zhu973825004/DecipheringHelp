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

namespace DecipheringHelp
{
    /// <summary>
    /// BindingTest.xaml 的交互逻辑
    /// </summary>
    public partial class BindingTest : Window
    {
        public NumberOfPlayers NumberOfPlayers = new NumberOfPlayers();
        public BindingTest()
        {
            InitializeComponent();
            NumberOfPlayers.Add(1);
            NumberOfPlayers.Add(2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NumberOfPlayers test = canvas1.Resources["numberOfPlayers"] as NumberOfPlayers;
            test.Add(4);
        }
    }
    public class NumberOfPlayers : ObservableCollection<int>
    {

    }
}
