using HysteresisRegulator.Services;
using HysteresisRegulator.ViewModels;
using HysteresisRegulator.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HysteresisRegulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService();
            IWindowService windowService = new WindowService();
            MainViewModel mainViewModel = new MainViewModel(windowService, dialogService);
            windowService.ShowMainView(mainViewModel);
        }
    }
}
