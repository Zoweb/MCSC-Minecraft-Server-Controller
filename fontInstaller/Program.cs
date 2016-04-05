using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Environment;
using System.Security.Principal;

namespace fontInstaller
{
    class Program
    {

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            WindowsIdentity user = null;
            try
            {
                //get the currently logged in user
                user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            finally
            {
                if (user != null)
                    user.Dispose();
            }
            return isAdmin;
        }

        public static bool hasAdminPrivilages = IsUserAdministrator();

        static void Main(string[] args)
        {
            Console.Title = "Font Installer";
            /*if (args.Length == 3)
            {
                File.Copy(args[0],
    Path.Combine(GetFolderPath(SpecialFolder.Windows),
        "Fonts", args[0]));

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
                key.SetValue(args[2], args[1]);
                key.Close();
            } else
            {
                Console.WriteLine("");
                Console.WriteLine("  There are either not enough or too many arguments.");
                Console.WriteLine("  Proper syntax is: fontInstaller FILEFOLDER:FileFolder:END[FILEFOLDER] FILENAME:FontFileName DESCRIPTION:fontDescription");
                Console.WriteLine("    [PRESS ANY KEY TO EXIT]");
                Console.ReadKey();
            }*/
            if (args[0] == "--delete" && args[1].Length > 1 && hasAdminPrivilages)
            {
                if (!File.Exists(Path.Combine(GetFolderPath(SpecialFolder.Windows),
                    "Fonts\\" + args[1])))
                {
                    Console.WriteLine("File does not exist.");
                    Exit(0);
                }
                File.Delete(Path.Combine(GetFolderPath(SpecialFolder.Windows),
                    "Fonts\\" + args[1]));
                if (!File.Exists(Path.Combine(GetFolderPath(SpecialFolder.Windows),
                    "Fonts\\" + args[1])))
                {
                    Console.WriteLine("Font file " + args[1] + " deleted.");
                } else
                {
                    Console.WriteLine("Font file " + args[1] + " could not be deleted.");
                }
            }
            else if (hasAdminPrivilages)
            {
                string[] a = { "FILEFOLDER:" };
                string[] b = { ":END[FILEFOLDER]" };
                string FileFolder = String.Join(" ", args).Split(a, StringSplitOptions.None)[1].Split(b, StringSplitOptions.None)[0];

                string[] c = { "FILENAME:" };
                string[] d = { ":END[FILENAME]" };
                string FileName = String.Join(" ", args).ToString().Split(c, StringSplitOptions.None)[1].Split(d, StringSplitOptions.None)[0];

                string[] e = { "DESCRIPTION:" };
                string[] f = { ":END[DESCRIPTION]" };
                string Description = String.Join(" ", args).ToString().Split(e, StringSplitOptions.None)[1].Split(f, StringSplitOptions.None)[0];

                string[] g = { "OTHERS:" };
                string[] Others = String.Join(" ", args).ToString().Split(g, StringSplitOptions.None)[1].Split(' ');

                Console.WriteLine(FileFolder + " " + FileName + " " + Description);

                Console.WriteLine(Path.Combine(GetFolderPath(SpecialFolder.Windows),
                    "Fonts\\" + FileName));

                if (Others[0].Length < 1 || Others[0] == "wait:true")
                {
                    Console.WriteLine("Are you sure you want to continue? Press enter to install font " + Description);
                    Console.ReadLine();
                }

                File.Copy(FileFolder,
                    Path.Combine(GetFolderPath(SpecialFolder.Windows),
                    "Fonts\\" + FileName), true);

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
                key.SetValue(Description, FileName);
                key.Close();

                if (Others[0].Length < 1 || Others[0] == "wait:true")
                {
                    Console.WriteLine("The font " + Description + " has been installed. Press enter to continue...");
                    Console.ReadLine();
                } else
                {
                    Console.WriteLine("The font " + Description + " has been installed.");
                }
            } else
            {
                Console.WriteLine("FontInstaller must be run with admin privilages.");
            }
            //Console.ReadLine();

            //Console.ReadKey();

            //fontinstaller FILEFOLDER:D:\TestFileFolder:END[FILEFOLDER] FILENAME:minecraft.ttf:END[FILENAME] DESCRIPTION:Minecraftia Regular:END[DESCRIPTION]
        }
    }
}
