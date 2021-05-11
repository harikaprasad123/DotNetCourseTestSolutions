using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wenskarten.Model;
using Wenskarten.View;
using Wenskarten.ViewModel;

namespace Wenskarten
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WenskaartVM wenskaartVM = new WenskaartVM(new Wenskaart(null, string.Empty, "Arial", 10));

            WenskartWindow wenskaartWindow = new WenskartWindow
            {
                 DataContext= wenskaartVM
            };


            wenskaartWindow.Show();
        }
    }
}
