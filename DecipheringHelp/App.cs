using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DecipheringHelp
{
    class App
    {
        [STAThread]
        static void Main()
        {

            // 定义Application对象作为整个应用程序入口  

            Application app = new Application();

            // 通过Url的方式启动
            app.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            //app.StartupUri = new Uri("BindingTest.xaml", UriKind.Relative);
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
           
            app.Run();
            app.Activated += app_Activated;
            app.Exit += app_Exit;
        }
        static void app_Activated(object sender, EventArgs e)
        {

            //throw new NotImplementedException();

        }



        static void app_Exit(object sender, ExitEventArgs e)
        {

            //throw new NotImplementedException();

        }
    }
}
