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
using Test.ViewModels;

namespace Test.Views
{
    /// <summary>
    /// Interaction logic for EmployeeListView.xaml
    /// </summary>
    public partial class EmployeeGridView : UserControl
    {
        public EmployeeGridView()
        {
            InitializeComponent();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(sender is TextBox textBox)
            {
                textBox.IsReadOnly = false;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.IsReadOnly = true;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(sender is EmployeeGridView view)
            {
                if(view.DataContext is EmployeeGridViewModel viewModel)
                {
                    if (view.IsVisible)
                    {
                        viewModel.Init();
                    }
                    else
                    {
                        viewModel.Clear();
                    }
                }
            }
        }
    }
}
