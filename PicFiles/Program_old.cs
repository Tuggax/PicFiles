using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PicFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceDirectory = args[0];
            var copyInfos = new[] { 
                new KeyValuePair<string, string>(args[1], "*.jpg"), 
                new KeyValuePair<string, string>(args[2], "*.avi"),
                new KeyValuePair<string, string>(args[2], "*.mov"),
                new KeyValuePair<string, string>(args[2], "*.m4v")};

            foreach (var copyInfo in copyInfos)
            {
                foreach (string file in Directory.GetFiles(sourceDirectory, copyInfo.Value, SearchOption.AllDirectories).OrderBy(x => x))
                {
                    var s = file.Substring(sourceDirectory.Length, file.Length - sourceDirectory.Length).TrimStart('\\').TrimEnd('\\').Split('\\');

                    if (s == null || s.Count() != 2)
                        continue;

                    string d = s[0];
                    string f = s[1];

                    DateTime dt;

                    try
                    {
                        dt = DateTime.ParseExact(d, "yyyy_MM_dd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    string fileName = dt.ToString("yyyy_MM_dd") + "_" + f;
                    string fullPath = Path.Combine(copyInfo.Key, fileName);

                    if (!File.Exists(fullPath))
                    {
                        Console.WriteLine("Copying file to {0}", fullPath);
                        File.Copy(file, fullPath, false);
                    }
                }
            }
   
            //foreach (string file in Directory.GetFiles(args[0], "*.avi", SearchOption.AllDirectories))
            //{
            //    string s = file.Substring(args[0].Length, file.Length - args[0].Length);

            //    string[] filepath = Path.Combine(@"\", s).Split('\\');

            //    string d = filepath[1];
            //    string f = filepath[2];

            //    DateTime dt;

            //    try
            //    {
            //        dt = DateTime.ParseExact(d, "yyyy_MM_dd", System.Globalization.CultureInfo.InvariantCulture);
            //    }
            //    catch (Exception)
            //    {
            //        continue;
            //    }

            //    string newFile = Path.Combine(args[2], dt.ToString("yyyy_MM_dd") + "_" + f);

            //    if (!File.Exists(newFile))
            //    {
            //        File.Copy(file, newFile, false);
            //    }
            //}
            
        }
    }
}
