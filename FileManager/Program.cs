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
        private static int _num = 1, _page;
        private static string _disc;
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
            if (_num == 1)
            {
                //PrintDriverDisc(0);
            }

            do
            {
                string command = Console.ReadLine();
                Console.Clear();
                string[] com = command.Split(' ');
                switch (com[0])
                {
                    case "ls":
                        _page = int.Parse(com[2]);
                        PrintFolder(com[1], _page - 1);
                        break;

                    case "cp":
                        CopyDir(com[1], com[2]);
                        break;

                    case "rm":
                        DeleteDir(com[1]);
                        break;

                    case "file":
                        Inform(com[1]); // доделать
                        break;

                    default:
                        Console.WriteLine("Вы ввели неверное значение!");
                        break;
                }
            } while (true);
        }

        static string DeleteDir(string del)
        {
            if (!File.Exists(del))
            {
                string[] dir = Directory.GetFileSystemEntries(del, "*", SearchOption.AllDirectories);
                for (int i = 0; i < dir.Length; i++)
                {
                    if (File.Exists(dir[i])) File.Delete(dir[i]);
                    else if (Directory.Exists(dir[i])) return DeleteDir(dir[i]);
                }
                Directory.Delete(del);
            }
            else
            {
                File.Delete(del);
            }
            return null;
        }
        static void CopyDir(string copy, string paste)
        {
            try
            {
                if (!Directory.Exists(copy))
                {
                    File.Copy(copy, paste);
                }
                else
                {
                    Directory.CreateDirectory(paste);
                    foreach (string s1 in Directory.GetFiles(copy))
                    {
                        string s2 = paste + "\\" + Path.GetFileName(s1);
                        File.Copy(s1, s2);
                    }
                    foreach (string s in Directory.GetDirectories(copy))
                    {
                        CopyDir(s, paste + "\\" + Path.GetFileName(s));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void PrintFolder(string fol, int npage)
        {
            string[] folders = Directory.GetDirectories(fol);
            string[] files = Directory.GetFiles(fol);

            int page = (folders.Length + files.Length) % 5;

            string[,] mass = new string[page, 6];

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
            catch (Exception e)
            {
                Console.WriteLine($"Вы ввели некооректную страницу {e.Message}.");
                throw;
            }
            Form();
            Console.WriteLine($"страница:{npage + 1}...{page}");
        }

        static void Inform(string ask) // Печать информации в папках и файлах
        {

            Form();
        }

        static void PrintDriverDisc(int i) // Печать диска и его сохранение
        {
            DriveInfo[] direc = DriveInfo.GetDrives();

            if (_num != 1)
            {
                Console.Clear();
                _disc = direc[i].ToString(); // _disc показывать в каком каталоге мы находимся
                return;
            }
            else
            {
                foreach (var print in direc)
                {
                    Console.WriteLine($"{_num++}- {print}");
                }
                Form();
            }
            Console.WriteLine("Приветствую в программе: Файловый менеджер! \nтут будет отобраться файлы и их информация");
        }
    }
}
