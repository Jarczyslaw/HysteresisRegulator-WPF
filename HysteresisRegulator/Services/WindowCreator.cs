using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HysteresisRegulator.Services
{
    public class WindowCreator
    {
        public void ShowWindow<T>(object viewModel) where T: Window, new()
        {
            var newWindow = CreateNewWindow<T>(viewModel);
            newWindow.Show();
        }

        public void ShowWindowAsDialog<T>(object viewModel) where T : Window, new()
        {
            var newWindow = CreateNewWindow<T>(viewModel);
            newWindow.ShowDialog();
        }

        private T CreateNewWindow<T>(object viewModel) where T : Window, new()
        {
            var newWindow = new T();
            newWindow.DataContext = viewModel;
            return newWindow;
        }
    }
}
