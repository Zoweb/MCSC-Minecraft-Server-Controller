using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using DotNetBrowser;
using DotNetBrowser.WinForms;
using DotNetBrowser.Events;
using DotNetBrowser.DOM;
using DotNetBrowser.DOM.Events;

namespace MinecraftServer
{
    public partial class MOTDEdit : Form
    {
        public MOTDEdit()
        {
            InitializeComponent();
            //motdbox.Font = new Font("Minecraftia", 18, FontStyle.Regular);

            Browser browser = BrowserFactory.Create();
            BrowserView view = new WinFormsBrowserView(browser);
            Controls.Add((Control)view);
            browser.DialogHandler = new CustomDialogHandler((Control)view, SynchronizationContext.Current);
            browser.LoadURL("http://localhost/motd");

            browser.FinishLoadingFrameEvent += delegate (object sender, FinishLoadingEventArgs e)
            {
                DOMDocument document = e.Browser.GetDocument();
                DOMElement o = document.GetElementById("out");
                DOMElement c = document.GetElementById("editor");
                c.AddEventListener(DOMEventType.OnKeyUp, delegate(object send, DOMEventArgs ev)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        //motdOutput.Text = o.InnerHTML.Replace("&amp;", "&").Replace("&nbsp;", " ").Replace("\n", Environment.NewLine);
                    }));
                }, false);
            };
        }

        private void MOTDEdit_Load(object sender, EventArgs e)
        {
            msgBox mbox = new msgBox();
            mbox.SetMsg("Please note that this editor is not perfect so will sometimes make mistakes. If it does, just hover over a button, see what it inserts, and type that in. You will have to do the same for styles, as they are unfortunately not available in the MinecraftServer Beta.", "MinecraftServer - Notice", "Message from Webpage");
            mbox.Show();
        }

        private void MOTDEdit_Close(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //Globals.FadeOut(this, 10, true);
        }

        /*private void motdbox_TextChanged(object sender, EventArgs e)
        {
            string[] split = new string[] { "\n" };
            if (motdbox.Text.Split(split, StringSplitOptions.None).Length > 2)
            {
                motdbox.Text = motdbox.Text.Split(split, StringSplitOptions.None)[0] + "\n" + motdbox.Text.Split(split, StringSplitOptions.None)[1];
                motdbox.SelectionStart = motdbox.Text.Length;
            }
        }*/
    }
}
