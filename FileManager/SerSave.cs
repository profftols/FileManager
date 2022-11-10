using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace FileManager
{
    [Serializable]
    class SerSave
    {
        public int Intepage { get; set; } = 25;
        public string Savedir { get; set; }
    }
}
