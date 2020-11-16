using System;
using System.Collections.Generic;
using System.Text;
using BLL;

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
                if(func.ToLower().Equals("exit"))
                {
                    exit = true; 
                }
                else
                {
                    if(func.ToLower().Equals("add"))
                    {
                        Add(); 
                    }
                }
            }
         }

        private string _menuEntry { get; } = "What do you want to do?\nAdd new money event\nChange info\nGenerate data info"; 

        private void Add()
        {
            Console.WriteLine("Select bill");
            
        }
    }
}
