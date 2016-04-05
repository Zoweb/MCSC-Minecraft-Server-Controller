using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using MinecraftServer;

namespace MinecraftServer
{
    public partial class newServer : Form
    {
        public newServer()
        {
            InitializeComponent();
        }

        download DlBox;

        private void okButton_Click(object sender, EventArgs e)
        {
            if (serverType.Text == "Spigot/BuildCraft")
            {
                string oldText = serverName.Text;
                serverName.Text = serverName.Text.Replace(' ', '_').Replace('\\', '_').Replace('/', '_');
                // https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar
                WebClient Client = new WebClient();
                Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\" + serverName.Text + @"\"));
                Client.DownloadFileAsync(new Uri("https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MinecraftServer\Servers\" + serverName.Text + @"\server.jar"));
                DlBox = new download();
                serverName.Text = oldText;
                DlBox.ShowDialog();
            }
        }

        private void serverName_TextChanged(object sender, EventArgs e)
        {
            if (serverName.Text.Length < 5)
            {
                willBeSavedAs.Text = "Please enter a server name.";
                okButton.Enabled = false;
            } else
            {
                willBeSavedAs.Text = "(Will be saved as '" + serverName.Text.Replace(' ', '_').Replace('\\', '_').Replace('/', '_') + "')";
                okButton.Enabled = true;
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            DlBox.downloadAmount.Text = "Downloaded " + e.BytesReceived + "b of " + e.TotalBytesToReceive + "b";
            DlBox.downloadProgress.Value = int.Parse(Math.Truncate(percentage).ToString());
            globals.newServerName = serverName.Text;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DlBox.downloadAmount.Text = "Completed";
            DlBox.Close();
            base.Close();
        }
    }
}
