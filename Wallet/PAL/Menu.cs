using System;
using System.Collections.Generic;
using System.Text;
using BLL;

namespace PL
{
    public class Menu
    {
       private BillService service; 
       public Menu()
        {
            service = new BillService(); 
            Print();
        }

        public void Print()
        {
            bool exit = false; 
            while(exit != true)
            {
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
            
        }
    }
}
