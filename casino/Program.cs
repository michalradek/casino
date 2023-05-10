using Microsoft.VisualBasic.FileIO;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Casino
{
    class Program
    {
        private int Early_menu()
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
                    switch(w)
                    {
                        case 1:
                            return w;
                            
                        case 2:
                            return w;
                        default:
                            Console.WriteLine("Wybierz zgodnie z przedziałem");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Prosze wpisac tylko liczbe.");
                }
                
            }
            return 0;
        }
        int zaloguj_sie()
        {
            string? login = null;
            SecureString haslo = new SecureString();
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("Podaj login: ");
                login = Console.ReadLine();
                Console.WriteLine("Podaj haslo: ");
                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        haslo.AppendChar(key.KeyChar);
                        Console.Write("*");
                    }
                    else if (key.Key == ConsoleKey.Backspace && haslo.Length > 0)
                    {
                        haslo.RemoveAt(haslo.Length - 1);
                        Console.Write("\b \b");
                    }
                } while (key.Key != ConsoleKey.Enter);
                try
                {
                    using (SHA256 sha = SHA256.Create())
                    {
                        IntPtr unmanagedString = IntPtr.Zero;
                        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(haslo);
                        string? helper = Marshal.PtrToStringUni(unmanagedString);
                        byte[] hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(helper));
                        string pass = BitConverter.ToString(hashed).Replace("-", "").ToLower();
                        Console.WriteLine(pass);
                        Console.ReadKey();
                        string[] linie = File.ReadAllLines($"{login}.txt");
                        if (login == linie[0] && pass == linie[1])
                        {
                            loop = false;
                            //menu();
                            return 1;
                        }
                        else
                        {
                            Console.WriteLine("Niepoprawny login lub haslo.");
                            Console.ReadKey();
                        }
                    }
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
            return 0;
        }
        int menu()
        {
            Console.Clear();
            Console.WriteLine("1. Zagraj.");
            Console.WriteLine("2. Wyloguj.");
            if (int.TryParse(Console.ReadLine(), out int w))
            {
                switch (w)
                {
                    case 1:
                        //zagraj
                        return 1;
                    case 2:
                        return 2;
                    default:
                        Console.WriteLine("Wybierz zgodnie z zakresem");
                        break;
                }
            }
            else
                Console.WriteLine("Dozwolone są tylko liczby");
            return 0;
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            if (program.Early_menu() == 1)
            {
                if (program.zaloguj_sie() == 1)
                {
                    if (program.menu() == 1)
                    {
                        Console.WriteLine("chuj");
                        Console.ReadKey();
                    }
                    else if(program.menu() == 2)
                    {
                        program.zaloguj_sie();
                    }
                }
            }
            else if (program.Early_menu() == 2)
            {
                Environment.Exit(0);
            }
            else
            {

            }
        }        
    }
}