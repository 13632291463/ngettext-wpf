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

namespace NGettext.Wpf.Example
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow1 : Window
    {
        private readonly ChangeCultureCommand _target;
        //private readonly ICultureTracker _cultureTracker = Substitute.For<ICultureTracker>();

        public MainWindow1()
        {
            _target = new ChangeCultureCommand();
            //ChangeCultureCommand.CultureTracker = _cultureTracker;
            _target.Execute("de-DE");
            InitializeComponent();
           

     

        }
    }
}
