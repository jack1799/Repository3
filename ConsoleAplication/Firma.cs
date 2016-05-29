﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAplication
{
    [Serializable]
    public class Firma
    {
        public Firma() { }
        public Firma(string Fio, int Age, int Zarplata)
        {
            this.Fio = Fio;
            this.Age = Age;
            this.Zarplata = Zarplata;
        }
        public string Fio { get; set; }
        public int Age { get; set; }
        public int Zarplata { get; set; }
        public void Info()
        {
            Console.WriteLine("Инициаллы: " + Fio);
            Console.WriteLine("Возраст: " + Age);
            Console.WriteLine("Зарплата: " + Zarplata);
        }
    }
}
