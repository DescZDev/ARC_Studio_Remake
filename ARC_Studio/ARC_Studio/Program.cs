using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ARC_Studio
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Get the folder where the exe is located
            string exePath = AppDomain.CurrentDomain.BaseDirectory;

            // Create a folder for libraries inside the same directory
            string libsPath = Path.Combine(exePath, "Libs");

            if (!Directory.Exists(libsPath))
                Directory.CreateDirectory(libsPath);

            // Define which file types to move
            string[] extensions = { ".dll", ".pdb", ".xml" };

            // Move all matching files into Libs
            foreach (var file in Directory.GetFiles(exePath))
            {
                string ext = Path.GetExtension(file);
                if (extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                {
                    string destFile = Path.Combine(libsPath, Path.GetFileName(file));
                    if (!File.Exists(destFile))
                    {
                        File.Move(file, destFile);
                    }
                }
            }

            // Make sure .NET can still find those DLLs
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string dllName = new AssemblyName(args.Name).Name + ".dll";
                string dllPath = Path.Combine(libsPath, dllName);
                return File.Exists(dllPath) ? Assembly.LoadFrom(dllPath) : null;
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ARCStudio());
        }
    }
}

