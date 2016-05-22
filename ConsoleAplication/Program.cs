using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;



namespace ConsoleAplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            int z;
            FileStream xFile = new FileStream("option.ini", FileMode.OpenOrCreate);
            string text = null;
            using (StreamReader sr = new StreamReader(xFile))
            {
                text = sr.ReadToEnd();
            }
            IDictionary<string, Firma> FirmaDictionary = new Dictionary<string, Firma>();
            XmlSerializer xs = new XmlSerializer(typeof (Dictionary<string, Firma>));  // Ошибка
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                if (text.ToLower() == "xml")
                {
                    using (FileStream fs = new FileStream("Firma.xml", FileMode.OpenOrCreate))
                    {
                        Dictionary<string, Firma> newF = (Dictionary<string, Firma>)xs.Deserialize(fs);
                        foreach (var Firma in newF)
                        {
                            FirmaDictionary.Add(Firma.Key, new Firma(Firma.Value.Age, Firma.Value.Zarplata));
                        }
                    }
                }
                else
                {
                    using (FileStream fs = new FileStream("Firma.dat", FileMode.OpenOrCreate))
                    {
                        Dictionary<string, Firma> FirmaDictionar = (Dictionary<string, Firma>)bf.Deserialize(fs);
                        foreach (var Firma in FirmaDictionar)
                        {
                            FirmaDictionary.Add(Firma.Key, new Firma(Firma.Value.Age, Firma.Value.Zarplata));
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            while (true)
            {
                Console.WriteLine("1 - создать запись сотрудника");
                Console.WriteLine("2 - удалить запись сотрудника");
                Console.WriteLine("3 - посмотреть все записи сотрудников");
                Console.WriteLine("4 - посмотреть записи сотрудника");
                Console.WriteLine();
                Console.WriteLine("0 - выйти из программы");
                char key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case '1':
                        Console.Clear();
                        Console.Write("Инициаллы: ");
                        string n = Console.ReadLine();
                        Console.Write("Возраст: ");
                        try
                        {
                            a = Int32.Parse(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Неверный ввод");
                            break;
                        }
                        Console.Write("Зарплата: ");
                        try
                        {
                            z = Int32.Parse(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Неверный ввод");
                            break;
                        }
                        try
                        {
                            FirmaDictionary.Add(n, new Firma(a, z));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Работник с такими же инициалами есть в базе");
                            Console.WriteLine("Нажмите любую клавишу для продолжения");
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine("Напишите имя для удаления из базы");
                        string g = (string)Console.ReadLine();
                        FirmaDictionary.Remove(g);
                        Console.Clear();
                        break;

                    case '3':
                        Console.Clear();
                        foreach (var firma in FirmaDictionary)
                        {
                            Console.WriteLine("Работник " + firma.Key);
                            firma.Value.Info();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Для продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case '4':
                        Console.Clear();
                        Console.WriteLine("Введите имя работника");
                        string f = (string)Console.ReadLine();
                        Console.Clear();
                        
                        Console.WriteLine("Данные про " + f );
                        try
                        {
                            FirmaDictionary[f].Info();
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Такого работника нет в базе");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Для продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case '0':
                        Console.Clear();
                        if (text.ToLower() == "xml")
                        {
                            using (FileStream fs = new FileStream("Firma.xml", FileMode.OpenOrCreate))
                            {
                                xs.Serialize(fs, FirmaDictionary);
                            }
                        }
                        else
                        {
                            using (FileStream fs = new FileStream("Firma.dat", FileMode.OpenOrCreate))
                            {
                                bf.Serialize(fs, FirmaDictionary);
                            }
                        }
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Нажмите клавишу от 0,1,2,3,4 в зависимости от нужного действия");
                        Console.WriteLine();
                        break;
                }                
            }
        }
    }
    [Serializable]
    public class Firma
    {
        public Firma() {}
        public Firma(int Age, int Zarplata)
        {
            this.Age = Age;
            this.Zarplata = Zarplata;
        }
        public int Age { get; set; }
        public int Zarplata { get; set; }
        public void Info()
        {
            Console.WriteLine("Возраст: " + Age);
            Console.WriteLine("Зарплата: " + Zarplata);
        }
    }
}
