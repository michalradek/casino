using Microsoft.VisualBasic.FileIO;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Casino
{
    public class Casino
    {
        protected string? login = "";
        protected SecureString haslo = new SecureString();
        public void Early_menu()
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
                            zaloguj_sie();
                            break;

                        case 2:
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Prosze wpisac tylko liczbe.");
                }

            }
        }
        private void zaloguj_sie()
        {
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
                        string[] linie = File.ReadAllLines($"{login}.txt");
                        if (login == linie[0] && pass == linie[1])
                        {
                            loop = false;
                            menu();
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
        }
        private void menu()
        {
            bool loop = true;
            while(loop)
            {
                Console.Clear();
                Console.WriteLine("1. Zagraj.");
                Console.WriteLine("2. Wyloguj.");
                if (int.TryParse(Console.ReadLine(), out int w))
                {
                    switch (w)
                    {
                        case 1:
                            Console.WriteLine("play");
                            break;
                        case 2:
                            Console.WriteLine("logout");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Dozwolone są tylko liczby");
                    Console.ReadKey();
                }         
            }
        }
    }
    class Program
    { 
        public static void Main(string[] args)
        {
            Casino casino = new Casino();
            casino.Early_menu();
        }
    }
}