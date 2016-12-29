using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ChannelOrderDownloads
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            Program.ConfigureLogger();
        }

        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
#if !DEBUG

            using (var sc = new ServiceController(serviceInstaller1.ServiceName))
            {
                Log.Verbose("Service installed");


                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                sc.Start();
                Log.Verbose("Service started");
            }
#endif
        }

        private void ProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
#if !DEBUG

            using (var sc = new ServiceController(serviceInstaller1.ServiceName))
            {
                Log.Verbose("Service uninstalling");
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    Log.Verbose("Service stopped by uninstaller");
                }
            }
#endif
        }
    }
}
