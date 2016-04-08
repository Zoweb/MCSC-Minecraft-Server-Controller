using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections;
using Microsoft.WindowsAPICodePack;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.ShellExtensions;
using Microsoft.WindowsAPICodePack.ExtendedLinguisticServices;
using Microsoft.WindowsAPICodePack.Sensors;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Threading;
using System.Diagnostics;
using MinecraftServer;
using System.Runtime.InteropServices;


namespace MinecraftServer
{
    public partial class home : Form
    {
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource(
    [In][MarshalAs(UnmanagedType.LPWStr)]
        string lpFileName);

        public home()
        {
            InitializeComponent();
        }

        download DlBox;

        private void updateBungee_Click(object sender, EventArgs e)
        {
            // http://dev.bukkit.org/media/files/892/229/sexymotd-1.3.1.jar
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\plugins\ServerListPlus"));
            WebClient Client = new WebClient();
            Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            Client.DownloadFileAsync(new Uri("http://ci.md-5.net/job/BungeeCord/lastSuccessfulBuild/artifact/bootstrap/target/BungeeCord.jar"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\server.jar"));
            DlBox = new download();
            DlBox.ShowDialog();
        }

        private void serverDeleted(object source, FileSystemEventArgs e)
        {
            MessageBox.Show("Server folder '" + e.FullPath.Split('\\')[e.FullPath.Split('\\').Length - 1] + "' was unexpectedly deleted." + Environment.NewLine + "This version of MinecraftServer unfortunatly does not have the ability to restore the folder, so if you don't want to lose your settings, undo the deletion. Otherwise, the program may continue running as normal, but if it doesn't, try restarting it or undoing the deletion.", "MinecraftServer - Error");
            Invoke(new MethodInvoker(delegate
            {
                checkServers();
            }));
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            DlBox.downloadAmount.Text = "Downloaded " + e.BytesReceived + "b of " + e.TotalBytesToReceive + "b";
            DlBox.downloadProgress.Value = int.Parse(Math.Truncate(percentage).ToString());
            var prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.Normal);
            prog.SetProgressValue(e.ProgressPercentage, 100);
        }

        void client_DownloadFileCompletedPlugin1(object sender, AsyncCompletedEventArgs e)
        {
            startStopBungee.PerformClick();
            var prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.NoProgress);
            DlBox.downloadAmount.Text = "Downloading config: ServerListPlus.yml";
            WebClient Client = new WebClient();
            Client.DownloadFileAsync(new Uri("http://zoweb.me/products/minecraftserver/defaultplugins/configfiles/slp"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\plugins\ServerListPlus\ServerListPlus.yml"));
            Thread sleep = new Thread(sleep10);
            sleep.Start();
        }

        void sleep10()
        {
            Thread.Sleep(5000);
            startStopBungee.Invoke(new MethodInvoker(delegate
            {
                bungee.StandardInput.WriteLine("/serverlistplus enable");
                startStopBungee.PerformClick();
            }));
            //File.Move(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\plugins\motd.jar"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\plugins\motd.jar"));
            //Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\plugins"));
            Thread.Sleep(1000);
            startStopBungee.Invoke(new MethodInvoker(delegate
            {
                //startStopBungee.PerformClick();
            }));
            Thread.Sleep(10000);
            startStopBungee.Invoke(new MethodInvoker(delegate
            {
                //startStopBungee.PerformClick();
            }));
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DlBox.downloadAmount.Text = "Installing default plugins... The server may run during this process.";
            Thread.Sleep(1000);
            WebClient Client = new WebClient();
            Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompletedPlugin1);
            Client.DownloadFileAsync(new Uri("http://zoweb.me/products/minecraftserver/defaultplugins/slp"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\plugins\slp.jar"));
            DlBox.Close();
            if(updateBungee.Text == "Download Bungee")
            {
                addToServerList("Bungee");
                updateBungee.Text = "Update Bungee";
                addServer.Enabled = true;
                startStopBungee.Enabled = true;
                optionsBox.Enabled = true;
            }
            var prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        private void addServer_Click(object sender, EventArgs e)
        {
            newServer choose = new newServer();
            choose.ShowDialog();
            /*if (globals.newServerName.Length > 0)
            {
                addToServerList(globals.newServerName);
                globals.newServerName = "";
            }*/
            checkServers();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void home_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers"));
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee\server.jar")))
            {
                startStopBungee.Enabled = true;
                optionsBox.Enabled = true;
            } else
            {
                updateBungee.Text = "Download Bungee";
                addServer.Enabled = false;
            }
            checkServers();
        }
        
        public string addToServerList(string name)
        {
            serverList.Items.Add(name);
            return name;
        }

        public void clearServerList()
        {
            serverList.Items.Clear();
        }

        public void checkServers()
        {
            var dirs = Directory.GetDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers"));
            clearServerList();
            foreach(string dir in dirs)
            {
                Console.WriteLine("FOUND DIRECTORY: " + dir);
                Console.WriteLine("-SPLIT VERSION:  " + dir.Split('\\')[dir.Split('\\').Length - 1]);
                Console.WriteLine("-VIEWED VERSION: " + dir.Split('\\')[dir.Split('\\').Length - 1].Replace('_', ' '));
                addToServerList(dir.Split('\\')[dir.Split('\\').Length - 1].Replace('_', ' '));
            }
            FileSystemWatcher watch = new FileSystemWatcher();
            watch.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers");
            watch.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                         | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watch.Filter = "*";
            watch.Deleted -= new FileSystemEventHandler(serverDeleted);
            watch.Deleted += new FileSystemEventHandler(serverDeleted);
            watch.EnableRaisingEvents = true;
        }

        public Process bungee = new Process();

        private void startStopBungee_Click(object sender, EventArgs e)
        {
            if (startStopBungee.Text == "Start")
            {
                ProcessStartInfo info = new ProcessStartInfo();

                info.CreateNoWindow = true;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                info.UseShellExecute = false;
                info.WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\Bungee");
                info.FileName = "java.exe";
                info.Arguments = "-Xms" + bungeeRAM.Text + "M -Xmx" + bungeeRAM.Text + "M -jar server.jar";

                bungee.StartInfo = info;

                bungee.OutputDataReceived += new DataReceivedEventHandler(bungee_update);
                bungee.ErrorDataReceived += new DataReceivedEventHandler(bungee_error);

                bungee.Start();

                bungee.BeginOutputReadLine();

                startStopBungee.Text = "Stop";
            } else
            {
                if (!bungee.HasExited)
                {
                    bungee.Kill();
                    startStopBungee.Text = "Start";
                }
                bungee = new Process();
            }
        }

        private void bungee_update(object sendingProcess, DataReceivedEventArgs d)
        {
            bungeeOutput.Invoke(new MethodInvoker(delegate
            {
                if (!String.IsNullOrEmpty(d.Data))
                {
                    bungeeOutput.Text += d.Data + Environment.NewLine;
                    bungeeOutput.SelectionStart = bungeeOutput.TextLength;
                    bungeeOutput.ScrollToCaret();
                }
            }));
        }

        private void bungee_error(object sendingProcess, DataReceivedEventArgs d)
        {
            MessageBox.Show("An Error Occured: " + Environment.NewLine + d.Data, "MinecraftServer");
        }

        private void MOTDEditor_Click(object sender, EventArgs e)
        {
            MOTDEdit edit = new MOTDEdit();
            using (Font fontTester = new Font(
                "Minecraftia",
                18,
                FontStyle.Regular,
                GraphicsUnit.Pixel))
            {
                if (fontTester.Name == "Minecraftia")
                {
                    edit.ShowDialog();
                }
                else
                {
                    var r = MessageBox.Show("It is reccomended that you install the Minecraft font before continuing. Press Yes to install, No to not install and continue, or Cancel to cancel the operation. Note: If you do not have admin privalages, you will have to enter an administrator's password.", "ZowebMinecraft", MessageBoxButtons.YesNoCancel);
                    if (r == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo()
                        {
                            Verb = "runas",
                            FileName = "fontInstaller.exe",
                            //Arguments = "--delete minecraft.ttf"
                            Arguments = "FILEFOLDER:" + Path.Combine(Directory.GetCurrentDirectory(), "minecraft.ttf") + ":END[FILEFOLDER] FILENAME:minecraft.ttf:END[FILENAME] DESCRIPTION:Minecraftia Regular:END[DESCRIPTION] other:wait:false"
                        });
                        edit.ShowDialog();
                    }
                    else if (r == DialogResult.No)
                    {
                        edit.ShowDialog();
                    }
                    else if (r == DialogResult.Cancel)
                    {

                    }
                }
            }
        }

        private void options_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bungeeRunCMD_Click(object sender, EventArgs e)
        {
            bungee.StandardInput.WriteLine(command.Text + "\n");
        }

        private void home_Close(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //Globals.FadeOut(this, 10, true, true);
        }
    }
}
