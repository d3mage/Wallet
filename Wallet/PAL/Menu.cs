using System;
using System.Collections.Generic;
using System.Text;

namespace PL
{
    public class Menu
    {
        Menu()
        {
            Print();
        }

        public void Print()
        {
            bool exit = false; 
            while(exit != true)
            {
                //UpdateMenu(); 
                Console.WriteLine(_menuEntry);
                string func = Console.ReadLine(); 
                switch (func.ToLower())
                {
                    case "exit":
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Enter legit thing");
                        break; 
                }
            }
         }

        private string _menuEntry { get; } = "Current "; 

    }
}
