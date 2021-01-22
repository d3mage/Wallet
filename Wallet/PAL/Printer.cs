using System;
using System.Collections.Generic;
using System.Text;

namespace PL
{
    static class Printer
    {
        public static void Print(List<string> list)
        {
            Console.WriteLine("\n#########\n");
            foreach(var l in list)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine("\n#########\n");
        }
    }
}
