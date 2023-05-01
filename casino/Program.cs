using System;
using System.IO;
namespace Casino
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("1. Zaloguj sie \n");
                Console.WriteLine("2. Wyjscie \n");
                Console.WriteLine("Wybor: ");
                if (int.TryParse(Console.ReadLine(), out int w))
                {
                    switch (w)
                    {
                        case 1:
                            loop = false;
                            zaloguj_sie();
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Wybierz 1 albo 2");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Wybierz liczbę.");
                    Console.ReadKey();
                }
            }
        }
        static void zaloguj_sie()
        {
            string? login = null;
            string? haslo = null;
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Podaj login: ");
                login = Console.ReadLine();
                Console.WriteLine("Podaj haslo: ");
                login = Console.ReadLine();
                try
                {
                    string[] linie = File.ReadAllLines($"{login}.txt");
                    if (login == linie[0] && haslo == linie[1])
                    {
                        //menu();
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawny login lub haslo.");
                        Console.ReadKey();
                    }
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
        }
    }
}