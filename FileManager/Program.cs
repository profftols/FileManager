using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Program
    {
        private static int num = 1;
        private static int page;
        private static string disc;
        private static string pathfolder;
        static void Form() //Поля для ограничения разделов
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }
        static void Main(string[] args) //                          >>>>> ТОЧКА ВХОДА <<<<<<<
        {
            MenuManager();
        }

        static void MenuManager() // Меню выбора ввода пользователя и передача методам выбор
        {
            if (num == 1)
            {
                PrintDriverDisc(0);
            }

            PrintDriverDisc(UserCh());
            PrintFolder(pathfolder, 1);

            do
            {
                string command = Console.ReadLine();
                string[] com = command.Split(' ');
                switch (com[0])
                {
                    case "ls":
                        page = int.Parse(com[3]);
                        PrintFolder(com[1], page);
                        break;

                    case "cp":

                        break;

                    case "rm":

                        break;

                    case "file":

                        break;

                    default:
                        Console.WriteLine("Вы ввели неверное значение!");
                        break;
                }
            } while (true);
        }

        static int UserCh()
        {
            Form();
            int i = int.Parse(Console.ReadLine());
            return i - 1;
        }

        static void PrintFolder(string fol, int npage)
        {
            string[] folders = Directory.GetDirectories(fol);
            int page = folders.Length % 7;



            Console.WriteLine("Папки:");
            for (int i = 0; i < folders.Length; i++)
            {
                Console.WriteLine(folders[i]);
            }

            Console.WriteLine($"Файлы в папке {fol}:");
            string[] files = Directory.GetFiles(fol);
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }
            Form();
        }

        static void Inform(string ask) // Печать информации в папках и файлах
        {

            Form();
        }

        static void PrintDriverDisc(int i) // Печать диска и его сохранение
        {
            DriveInfo[] direc = DriveInfo.GetDrives();

            if (num != 1)
            {
                Console.Clear();
                disc = direc[i].ToString();
                pathfolder = disc;
                return;
            }
            else
            {
                foreach (var print in direc)
                {
                    Console.WriteLine($"{num++}- {print}");
                }
                Form();
            }
            Console.WriteLine("Приветствую в программе: Файловый менеджер! \nтут будет отобраться файлы и их информация");
        }
    }
}
