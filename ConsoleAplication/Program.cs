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
            int a,z;
            bool i;
            FileStream xFile = new FileStream("option.ini", FileMode.OpenOrCreate);
            string text = null;
            using (StreamReader sr = new StreamReader(xFile))
            {
                text = sr.ReadToEnd();
            }
            List<Firma> FirmaList = new List<Firma>();
            XmlSerializer xs = new XmlSerializer(typeof (List<Firma>));
            BinaryFormatter bf = new BinaryFormatter();
            if (text.ToLower() == "xml")
            {
                try
                {
                    using (FileStream fs = new FileStream("Firma.xml", FileMode.OpenOrCreate))
                    {
                        List<Firma> newF = (List<Firma>)xs.Deserialize(fs);
                        foreach (Firma m in newF)
                        {
                            FirmaList.Add(new Firma(m.Fio, m.Age, m.Salary));
                        }
                    }
                }
                catch (Exception) { }
            }
            else
            {
                if (text.ToLower() != "bin")
                {
                    Console.WriteLine("В конфигурационном файле option.ini напишите bin или xml в зависимости от нужной сериализации (по умолчанию bin)");
                    Console.WriteLine();
                }
                try
                {
                   using (FileStream fs = new FileStream("Firma.dat", FileMode.OpenOrCreate))
                   {
                       List<Firma> FirmaL = (List<Firma>)bf.Deserialize(fs);
                       foreach (Firma m in FirmaL)
                       {
                           FirmaList.Add(new Firma(m.Fio, m.Age, m.Salary));
                       }
                   }
                }
            catch (Exception) { }
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

                        Boolean b = true;
                        foreach (var Firma in FirmaList)
                        {
                            if (Firma.Fio == n)
                            {
                                Console.Clear();
                                Console.WriteLine("Работник с такими же инициалами есть в базе");
                                Console.WriteLine("Нажмите любую клавишу для продолжения");
                                Console.ReadKey();
                                b = false;
                                break;
                            }
                        }
                        if (b) FirmaList.Add(new Firma(n, a, z));
                        Console.Clear();
                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine("Напишите имя для удаления из базы");
                        string g = Console.ReadLine();
                        i = true;
                        foreach (Firma m in FirmaList)
                        {
                            if (m.Fio == g)
                            {
                                FirmaList.Remove(m);
                                i = false;
                                break;
                            }
                        }
                        if (i)
                        {
                            Console.WriteLine("Такого имени нет в базе");
                        }
                        Console.Clear();
                        break;

                    case '3':
                        Console.Clear();
                        foreach (Firma m in FirmaList)
                        {
                            Console.WriteLine("Инициаллы: " + m.Fio);
                            Console.WriteLine("Возраст: " + m.Age);
                            Console.WriteLine("Зарплата: " + m.Salary);
                            Console.WriteLine();
                        }
                        Console.WriteLine("Для продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case '4':
                        Console.Clear();
                        Console.WriteLine("Введите имя работника");
                        string f = Console.ReadLine();
                        i = true;
                        foreach (Firma m in FirmaList)
                        {
                            if (m.Fio == f)
                            {
                                Console.Clear();
                                Console.WriteLine("Инициаллы: " + m.Fio);
                                Console.WriteLine("Возраст: " + m.Age);
                                Console.WriteLine("Зарплата: " + m.Salary);
                                i = false;
                            }
                        }
                        if (i)
                        {
                            Console.WriteLine("Такого имени нет в базе");
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
                                xs.Serialize(fs, FirmaList);
                            }
                        }
                        else
                        {
                            using (FileStream fs = new FileStream("Firma.dat", FileMode.OpenOrCreate))
                            {
                                bf.Serialize(fs, FirmaList);
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
}
