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

namespace EridanEditor.Utilities
{
    /// <summary>
    /// Logique d'interaction pour LoggerView.xaml
    /// </summary>
    public partial class LoggerView : UserControl
    {
        public LoggerView()
        {
            InitializeComponent();
        }

        private void OnClear_Button_Click(object sender, RoutedEventArgs e)
        {
            Logger.Clear();
        }

        private void OnMessageFilter_Button_Click(object sender, RoutedEventArgs e)
        {
            var mask = 0x0;
            if(toggleInfo.IsChecked == true) mask |= (int)MessageType.Info;
            if (toggleWarning.IsChecked == true) mask |= (int)MessageType.Warning;
            if (toggleError.IsChecked == true) mask |= (int)MessageType.Error;
            Logger.SetMessageFilter(mask);
        }
    }
}
