using System.Windows;

namespace NGettext.Wpf.Example
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            CompositionRoot.Compose("Example");
            //var  _target = new ChangeCultureCommand();
            //ChangeCultureCommand.CultureTracker = _cultureTracker;
            //_target.Execute("en-US");
            base.OnStartup(e);
        }
    }
}