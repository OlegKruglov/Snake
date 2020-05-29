using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using Snake_Kruglov;
using System.Globalization;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Файлы с Результатами находяться в  @..\Snake Kruglov\Snake Kruglov\bin\Debug под названием "Nimetus.txt" и "Points.txt" */
            Menu menu = new Menu();
            menu.MainMenu();
            Console.ReadLine();
        }

    }
}
