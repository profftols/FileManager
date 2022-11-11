using System;
using System.IO;

namespace FileManager
{
    [Serializable]
    class Logic
    {
        private int _page;
        private double Decipage { get; set; }
        public int ElementPrint { get; set; } = 25;
        public string Savedir { get; set; }

        public void MenuManager() // Меню выбора ввода пользователя и передача методам выбор
        {
            if (Savedir != null)
            {
                PrintFolder(Savedir, 0);
            }

            Decipage = ElementPrint;

            Form();
            do
            {
                string command = Console.ReadLine();
                Console.Clear();
                string[] com = command.Split(' ');
                try
                {
                    switch (com[0])
                    {
                        case "ls":
                            int npage = int.Parse(com[2]);
                            PrintFolder(com[1], npage - 1);
                            break;

                        case "cp":
                            CopyDir(com[1], com[2]);
                            break;

                        case "rm":
                            DeleteDir(com[1]);
                            break;

                        case "info":
                            Inform(com[1]);
                            break;

                        case "save":
                            return;

                        default:
                            PrintDriverDisc();
                            Console.WriteLine("Вы ввели неверную команду!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    RecordErr(e.Message);
                }

            } while (true);
        }

        public void DeleteDir(string del)
        {
            if (!File.Exists(del))
            {
                string[] dir = Directory.GetFileSystemEntries(del);

                foreach (var item in dir)
                {
                    if (Directory.Exists(item))
                    {
                        DeleteDir(item);
                    }
                    if (File.Exists(item))
                    {
                        File.Delete(item);
                    }
                }
                Directory.Delete(del);
            }
            else
            {
                File.Delete(del);
            }
        }

        public void CopyDir(string copy, string paste)
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
                RecordErr(e.Message);
            }
        }

        public void PrintFolder(string fol, int npage)
        {
            string[] folders = Directory.GetDirectories(fol);
            string[] files = Directory.GetFiles(fol);

            _page = (folders.Length + files.Length) / ElementPrint;
            double page = (folders.Length + files.Length) / Decipage;
            if (page > _page) _page++; //Сравниваем числа на целое или не целое, что бы дополнить десятичное число дополнительной страницей 

            string[,] mass = new string[_page, ElementPrint];

            for (int i = 0, k = 0, p = 0; i < mass.GetLength(0); i++)
            {
                for (int j = 0; j < mass.GetLength(1); j++, k++)
                {
                    if (k <= folders.Length - 1) // Реализация помещения папок в массив для постраничного ввода
                        mass[i, j] = folders[k];
                    else if (p <= files.Length - 1) // Аналогичный процесс для файлов если папки кончились
                    {
                        mass[i, j] = files[p];
                        p++;
                    }
                }
            }

            if (npage + 1 <= _page && npage >= 0)
            {
                Console.WriteLine($"Путь: {fol}");
                for (int i = 0; i < mass.GetLength(1); i++)
                {
                    try
                    {
                        if (mass[npage, i] == null) // пропуск пустык ячеек если суммарно файлов и папки не кратно делению на постраничный ввод
                            continue;

                        if (Directory.Exists(mass[npage, i]))
                        {
                            DirectoryInfo print = new DirectoryInfo(mass[npage, i]);
                            Console.WriteLine($"   --{print.Name}");

                            string[] inpage = Directory.GetDirectories(mass[npage, i]);
                            foreach (string pri in inpage)
                            {
                                DirectoryInfo prin = new DirectoryInfo(pri);
                                Console.WriteLine($"\t|_{prin.Name}"); // вывод папок в папке для красивого дерева и возможности зайти куда нибудь
                            }
                        }
                        else
                        {
                            FileInfo print = new FileInfo(mass[npage, i]);
                            Console.WriteLine($"   {print.Name}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\tСистемный файл или папка доступа - нет");
                        RecordErr(e.Message);
                    }
                }
            }
            else
            {
                PrintDriverDisc();
                Console.WriteLine("Вы ввели неправильную страницу");
            }
            Form();
            Console.WriteLine($"страница:{npage + 1}..из..{_page}");

            Savedir = fol;
        }

        public void Inform(string name) // Печать информации в папках и файлах
        {
            if (Directory.Exists(name))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(name);
                Console.WriteLine($"Название каталога: {dirInfo.Name} \nВремя создания каталога: {dirInfo.CreationTime} \nКорневой каталог: {dirInfo.Root} ");
            }
            else
            {
                FileInfo fileInfo = new FileInfo(name);
                Console.WriteLine($"Название файла: {fileInfo.FullName} \nРазмер файла: {fileInfo.Length} байт \nРасширение файла: {fileInfo.Extension}");
            }
        }

        public void PrintDriverDisc() // Печать диска и его сохранение
        {
            DriveInfo[] direc = DriveInfo.GetDrives();

            foreach (var print in direc)
                Console.WriteLine($"--{print}");

            Form();
        }

        private void RecordErr(string err)
        {
            DateTime now = DateTime.Now;
            File.AppendAllText("ErrorLog.txt", $"\n{now:f} --- {err}");
        }

        static void Form() //Поля для ограничения разделов
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }
    }
}
