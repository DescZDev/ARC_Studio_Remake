using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ARC_Studio.EnchUI
{
    public static class RecentFileManager
    {
        private static readonly string FilePath = Path.Combine(Application.StartupPath, "recent_files.txt");

        public static List<string> GetRecentFiles()
        {
            if (!File.Exists(FilePath)) return new List<string>();
            return File.ReadAllLines(FilePath)
                .Where(f => ! string.IsNullOrWhiteSpace(f))
                .Distinct()
                .ToList();
        }

        public static void Add(string file)
        {
            var list = GetRecentFiles();

            list.Remove(file);
            list.Insert(0, file);

            if (list.Count > 10)
                list.RemoveAt(10);

            File.WriteAllLines(FilePath, list);
        }
    }
}
