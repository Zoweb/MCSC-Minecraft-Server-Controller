
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace MinecraftServer
{
    class Globals
    {
        public static string newServerName;

        public async static void FadeIn(Form o, int interval = 80)
        {
            //Object is not fully invisible. Fade it in
            while (o.Opacity < 1.0)
            {
                await Task.Delay(interval);
                o.Opacity += 0.05;
            }
            o.Opacity = 1; //make fully visible       
        }

        public async static void FadeOut(Form o, int interval = 80, Boolean closeOnEnd = false, Boolean isMainWindow = false)
        {
            //Object is fully visible. Fade it out
            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0; //make fully invisible    
            //o.FormBorderStyle = 0;
            o.Close();
            if (isMainWindow)
            {
                Application.Exit();
            }
        }
    }
}
