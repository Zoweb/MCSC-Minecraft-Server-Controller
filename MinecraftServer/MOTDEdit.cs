using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skybound.Gecko;

namespace MinecraftServer
{
    public partial class MOTDEdit : Form
    {
        public MOTDEdit()
        {
            InitializeComponent();
            //motdbox.Font = new Font("Minecraftia", 18, FontStyle.Regular);
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
