using System;
using System.Collections.Generic;
using System.Text;
using BLL;

namespace PL
{
    public class Menu
    {
        private string _menuEntry  = "What do you want to do?\nAdd\nDelete\nChange info\nGenerate data stats";

        IGetInputService getInputService;
        ISecondaryMenu secondaryMenu;

       public Menu(IGetInputService getInput, ISecondaryMenu secondary)
        {
            getInputService = getInput;
            secondaryMenu = secondary; 
        }

        public int Print()
        {
            string func = "";
           
            while(!func.Equals("exit"))
            {
                Console.WriteLine(_menuEntry);
                try
                {
                    func = getInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                }
                catch (TooManyFalseAttemptsException e)
                {
                    Console.WriteLine(e.msg);
                }
                if (func.Equals("add"))
                {
                    secondaryMenu.Add(getInputService);
                    break; 
                }
                else if(func.Equals("delete"))
                {

                }
            }
            return 1;
         }

    }
}
