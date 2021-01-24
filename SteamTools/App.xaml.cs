using Octokit;
using SteamTools.Views;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace SteamTools
{
    partial class App : System.Windows.Application
    {
        internal static readonly GitHubClient gitHubClient = new GitHubClient(new ProductHeaderValue("SteamTools"));

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            new MainWindow().Show();
            await CheckForUpdates();
        }

        private async void Application_Exit(object sender, ExitEventArgs e) => await Task.Run(() => SteamClient.Shutdown());

        private static async Task CheckForUpdates()
        {
            IReadOnlyList<RepositoryTag> tags;
            try
            {
                tags = await gitHubClient.Repository.GetAllTags("Jochem-W", "SteamTools");
            }
            catch (Exception exception)
            {
                new MessageWindow("Update check failed!", "Encountered the following error while checking for updates:\n" +
                                                          $"{exception.Message}\n" +
                                                          "Are you connected to the internet?").ShowDialog();
                return;
            }
            var latestRelease = tags.First(t => !t.Name.Contains("pre"));
            var latestVersion = new Version(latestRelease.Name.Remove(0, 1));
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            if (currentVersion.CompareTo(latestVersion) < 0)
            {
                new MessageWindow("Update available",
                                  "A new version of SteamTools is available\nClicking the 'Ok' button will take you to the downloads.",
                                  new Command(_ => Process.Start(new ProcessStartInfo
                                  {
                                      FileName = "https://github.com/Jochem-W/SteamTools/releases",
                                      UseShellExecute = true
                                  }))).ShowDialog();
            }
        }
    }
}
