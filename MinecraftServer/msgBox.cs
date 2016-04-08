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
using System.Timers;

namespace MinecraftServer
{
    public partial class msgBox : Form
    {

        public msgBox()
        {
            InitializeComponent();
        }

        private void msgBox_Load(object sender, EventArgs e)
        {

        }

        string messg = "Error";

        public void SetMsg(string msg, string title = " ", string ActualTitle = " ")
        {
            messg = msg;
            Title.Text = title;
            message.Text = msg;
            Text = ActualTitle;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void msgBox_close(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //Globals.FadeOut(this, 10, true);
            FormBorderStyle = 0;
        }

        private void message_TextChanged(object sender, EventArgs e)
        {
            message.Text = messg;
        }

        private void message_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
