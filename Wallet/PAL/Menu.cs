using System;
using System.Collections.Generic;
using System.Text;

namespace PL
{
    public class Menu
    {
       public  Menu()
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
                    case "profit": 

                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Enter legit thing");
                        break; 
                }
            }
         }

        private string _menuEntry { get; } = "What do you want to do?\nAdd profit\nAdd expense\nGenerate data info"; 

    }
}
