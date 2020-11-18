using System;
using System.Collections.Generic;
using System.Text;
using BLL;

namespace PL
{
    public class Menu
    {
        private string _menuEntry  = "What do you want to do?\nAdd new money event\nChange info\nGenerate data info";

        IGetInputService getInputService; 

       public Menu(IGetInputService getInput)
        {
            getInputService = getInput; 
        }

        public void Print()
        {
            bool exit = false; 
            while(exit != true)
            {
                string func = "";
                Console.WriteLine(_menuEntry);
                try
                {
                    func = getInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                }
                catch (TooManyFalseAttemptsException e)
                {
                    Console.WriteLine(e.msg);
                }

                if (func.Equals("exit"))
                {
                    exit = true; 
                }
                else
                {
                    if(func.Equals("add"))
                    {

                    }
                }
            }
            return;
         }


        public void Add()
        {

        }
    }
}
