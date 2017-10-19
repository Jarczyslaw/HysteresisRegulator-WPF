using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HysteresisRegulator.Views;

namespace HysteresisRegulator.Services
{
    public class WindowService : IWindowService
    {
        private WindowCreator creator;

        public WindowService()
        {
            creator = new WindowCreator();
        }

        public void ShowHelpView()
        {
            creator.ShowWindowAsDialog<HelpView>(null);
        }

        public void ShowMainView(object viewModel)
        {
            creator.ShowWindow<MainView>(viewModel);
        }
    }
}
