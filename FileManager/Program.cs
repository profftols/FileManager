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

            do
            {
                string command = Console.ReadLine();
                string[] com = command.Split(' ');
                switch (com[0])
                {
                    case "ls":
                        page = int.Parse(com[3]);
                        PrintFolder(com[1], page-1);
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

        /*static int UserCh()
        {
            Form();
            int i = int.Parse(Console.ReadLine());
            return i - 1;
        }*/

        static void PrintFolder(string fol, int npage)
        {
            string[] folders = Directory.GetDirectories(fol);
            string[] files = Directory.GetFiles(fol);
            int page = (folders.Length + files.Length) % 5;

            string[,] mass = new string [page, 6];

            for (int i = 0, k = 0, p = 0; i < mass.GetLength(0); i++)
            {
                for (int j = 0; j < mass.GetLength(1); j++, k++)
                {
                    if (k <= folders.Length - 1)
                        mass[i, j] = folders[k];
                    else if (p <= files.Length - 1)
                    {
                        mass[i, j] = files[p];
                        p++;
                    }
                }
            }
            try
            {
                for (int i = 0; i < mass.GetLength(1); i++)
                {
                    if (mass[npage, i] == null)
                        return;
                    Console.WriteLine(mass[npage, i]);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Вы ввели некооректную страницу.");
                throw;
            }
            Form();
            Console.WriteLine($"страница:{npage+1}...{page}");
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
