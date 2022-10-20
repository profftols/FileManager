using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.IO;
using System.Diagnostics;

namespace FileManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ask = "D:\\";
            Form();
            FileTree(ask);
            RequestUser();
        }

        static int RequestUser()
        {
            int choice = 1;
            switch (choice)
            {
                case 1:
                    
                default:
                    break;
            }
            return choice;
        }

        static void Form()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }

        static void FileTree(string ask)
        {
            int num = 1;
            DriveInfo[] direc = DriveInfo.GetDrives();
            foreach (var print in direc)
            {
                Console.WriteLine($"{num++}- {print}");
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
