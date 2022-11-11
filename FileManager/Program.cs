using System;
using System.IO;
using System.Text.Json;

namespace FileManager
{
    public class Program
    {
        private const string _nameJsonFile = "config.json";

        static void Main()
        {

            Logic serSave = new Logic();

            try
            {
                string objectdesir = File.ReadAllText(_nameJsonFile);
                serSave = JsonSerializer.Deserialize<Logic>(objectdesir);
            }
            catch (Exception)
            {
                serSave.PrintDriverDisc();
                Console.WriteLine("Приветствую в программе файловый менеджер, введите команду для заходи в диск");
            }

            serSave.MenuManager();

            string json = JsonSerializer.Serialize(serSave);
            File.WriteAllText(_nameJsonFile, json);
        }

    }
}
