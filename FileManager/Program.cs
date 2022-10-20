using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.IO;


namespace FileManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ask = "D:\\";
            Form();
            FileTree(ask);
        }

        static void Form()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }

        static void FileTree(string ask)
        {
            string[] direc = Directory.GetFileSystemEntries(ask);
            foreach (string print in direc)
            {
                Console.WriteLine(print);
            }
            Form();
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(ask);
            foreach (string s in files)
            {
                Console.WriteLine(s);
            }
        }
    }
}
