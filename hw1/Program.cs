using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string extension;
            string text;
            if (args.Length != 0)
                foreach (var s in args)
                    Console.WriteLine(s);
            if (args.Length == 2)
            {
                extension = args[0];
                text = args[1];
            }
            else
            {
                extension = "txt";
                text = "qwerty";
            }

            findFileByExtensionAndText(new DirectoryInfo(Directory.GetCurrentDirectory()), extension, "qwerty");
            Console.ReadLine();
        }
        public static void findFile(DirectoryInfo dir, string fileName)
        {
            if (!dir.Exists)
                return;
            else
            {
                foreach (var item in dir.EnumerateFiles())
                {
                    if (item.Name == fileName)
                        Console.WriteLine($"Файл {fileName} найден в {dir.FullName}");
                }

                foreach (var item in dir.EnumerateDirectories())
                {
                    findFile(item, fileName);
                }
            }
        }
        public static void findFileByExtensionAndText(DirectoryInfo dir, string extension, string text)
        {
            if (!dir.Exists)
                return;
            else
            {
                foreach (var item in dir.EnumerateFiles("*." + extension))
                {
                    using (var fr = new StreamReader(item.FullName))
                    {
                        if (fr.ReadToEnd().Contains(text))
                            Console.WriteLine($"Файл {item.Name} найден в {dir.FullName}");
                    }
                }

                foreach (var item in dir.EnumerateDirectories())
                {
                    findFileByExtensionAndText(item, extension, text);
                }
            }
        }
    }
}
