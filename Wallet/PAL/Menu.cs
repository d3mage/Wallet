using System;
using System.Collections.Generic;
using System.Text;

namespace PL
{
    public class Menu
    {
        void Print()
        {
            bool exit = false; 
            while(exit != true)
            {
                //UpdateMenu(); 
                Console.WriteLine(_menuEntry);
            }
         }

        private string _menuEntry { get; } = "Current "; 

    }
}
