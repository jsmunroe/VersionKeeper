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

namespace VersionKeeper.WPF
{
    /// <summary>
    /// Interaction logic for SearchAssembliesWindow.xaml
    /// </summary>
    public partial class SearchAssembliesWindow : Window
    {
        public SearchAssembliesWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = VisualTreeHelper.GetParent((DependencyObject) sender) as ScrollViewer;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
        }
    }
}
