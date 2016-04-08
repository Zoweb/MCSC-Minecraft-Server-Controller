using DotNetBrowser;
using DotNetBrowser.WinForms;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MinecraftServer
{
    class CustomDialogHandler : DialogHandler
    {
        private WinFormsDefaultDialogHandler handler;
        private Control control;
        private SynchronizationContext synchronizationContext;

        public CustomDialogHandler(Control control, System.Threading.SynchronizationContext synchronizationContext)
        {
            this.control = control;
            this.synchronizationContext = synchronizationContext;
            handler = new WinFormsDefaultDialogHandler(control);
        }

        public void OnAlert(DialogParams parameters)
        {
            synchronizationContext.Send(new SendOrPostCallback(
               delegate (object state)
               {
                   string url = parameters.Url;
                   string url2 = url.Split('/')[2];
                   //String title = "Message from \"" + url + "\"";
                   string message = parameters.Message;
                   msgBox msg = new msgBox();
                   msg.SetMsg(message);//"Message from " + url2, "Message from Webpage");
                   msg.ShowDialog();
                   /*String url = parameters.Url;
                   String title = "Message from \"" + url + "\"";
                   String message = parameters.Message;
                   MessageBox.Show(control, message, title);*/
               }), parameters);
        }

        public CloseStatus OnBeforeUnload(UnloadDialogParams parameters)
        {
            return handler.OnBeforeUnload(parameters);
        }

        public CloseStatus OnConfirmation(DialogParams parameters)
        {
            return handler.OnConfirmation(parameters);
        }

        public CloseStatus OnFileChooser(FileChooserParams parameters)
        {
            return handler.OnFileChooser(parameters);
        }

        public CloseStatus OnPrompt(PromptDialogParams parameters)
        {
            return handler.OnPrompt(parameters);
        }

        public CloseStatus OnReloadPostData(ReloadPostDataParams parameters)
        {
            return handler.OnReloadPostData(parameters);
        }

        public CloseStatus OnSelectCertificate(CertificatesDialogParams parameters)
        {
            return handler.OnSelectCertificate(parameters);
        }
    }
}