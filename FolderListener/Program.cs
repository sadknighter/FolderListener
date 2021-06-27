using System;
using Topshelf;

namespace BCLTaskApp
{
    class Program
    {
        static void Main()
        {
            var rc = HostFactory.Run(x =>
                {
                    x.Service<FileSystemMonitor>(s =>
                        {
                            s.ConstructUsing(name => new FileSystemMonitor());
                            s.BeforeStartingService(() => new CultureSettings().SetCultureInfo());
                            s.WhenStarted(tc => tc.Start());
                            s.WhenStopped(tc => tc.Stop());
                        });

                    x.EnableServiceRecovery(r =>
                        {
                            r.RestartService(0);
                        });

                    x.RunAsLocalSystem();

                    x.SetDescription(Resources.Resources.FileWatcherServiceHost);
                    x.SetDisplayName(Resources.Resources.FileWatcherServiceDisplayName);
                    x.SetServiceName(Resources.Resources.FileWatcherService);

                    x.EnableShutdown();
                    x.UseNLog();
                });

            var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
