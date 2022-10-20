using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace FileManager
{
    internal class Program
    {
        static int num = 1;
        static void Main(string[] args)
        {
            PrintDriver(0); // вывод на консоль каталога накопителей
            MenuManager();

            Console.WriteLine("Программа завершилась, нажмите Enter");
            Console.ReadKey();
        }

        static void MenuManager()
        {
            try
            {
                int chce = int.Parse(Console.ReadLine());
                PrintDriver(chce);

            }
            catch
            {
                Console.WriteLine($"Ошибка! Вы не выбрали значение от 1 до {num} ");
            }
            finally { num = 1; }
        }

        static void PrintInformation(string ask)
        {
            string[] files = Directory.GetFiles(ask);

            foreach (var item in files)
            {
                Console.WriteLine(item);
            }
            Form();
        }

        static void Form() //Поля для ограничения разделов
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }

        static void PrintDriver(int choice)
        {
            DriveInfo[] direc = DriveInfo.GetDrives();

            if (choice != 0)
            {


                for (int i = 0; i < direc.Length; i++)
                {
                    
                }
            }
            else
            {
                foreach (var print in direc)
                {
                    Console.WriteLine($"{num++}- {print}");
                }
            }
            Form();
        }
    }
}
