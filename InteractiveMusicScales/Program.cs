using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace InteractiveMusicScales
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //Apply culture parameter if any
            if(args != null && args.Length > 0)
            {
                string cultureName = args[0].Remove(0,1);

                try
                {
                    var culture = new CultureInfo(cultureName);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
                catch (Exception ex)
                {
                    Logger.LogTheException(ex);
                }
            }


            //Run the app
            var app = new App();

            app.DispatcherUnhandledException += (sender, excArgs) => { Logger.LogTheException(excArgs.Exception); excArgs.Handled = true; };

            var window = new MainWindow();

            app.MainWindow = window;

            var interfacedata = new InterfaceData();
            window.DataContext = interfacedata;

            var scalesmanager = new ScalesManager();
            interfacedata.Request_LoadAdditionalScales += scalesmanager.Handle_LoadAdditionalScalesRequest;
            interfacedata.Request_SaveAdditionalScales += scalesmanager.Handle_SaveAdditionalScalesRequest;

            var settingsmanager = new SettingsManager();
            interfacedata.Request_LoadSettings += settingsmanager.Handle_LoadSettingsRequest;
            interfacedata.Request_SaveSettings += settingsmanager.Handle_SaveSettingsRequest;

            var localizationmanager = new LocalizationManager(defaultCultureName:"en-US");
            interfacedata.Request_LoadLocalization += localizationmanager.Handle_LoadLocalizationRequest;

            var xmlLoaderSaver = new XmlLoaderSaver();
            scalesmanager.Request_Load += xmlLoaderSaver.Handle_LoadRequest;
            scalesmanager.Request_Save += xmlLoaderSaver.Handle_SaveRequest;
            settingsmanager.Request_Load += xmlLoaderSaver.Handle_LoadRequest;
            settingsmanager.Request_Save += xmlLoaderSaver.Handle_SaveRequest;
            localizationmanager.Request_Load += xmlLoaderSaver.Handle_LoadRequest;

            app.InitializeComponent();
            app.Run();
        }
    }
}
