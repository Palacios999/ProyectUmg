using static System.Console;

namespace ProyectoFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            int optionMenu = 0;

            while (true)
            {
                if (showMenu)
                {
                    optionMenu = MenusModule.FirstMenu();
                }

                switch (optionMenu)
                {
                    case 1:
                        MenusModule.InitGame();
                        break;
                    case 2:
                        MenusModule.PrintInstructions();
                        showMenu = true;
                        break;
                    case 3:
                        MenusModule.ViewInfoCristal();
                        showMenu = true;
                        break;
                    case 4:
                        WriteLine("¡Hasta luego!");
                        return;
                }
            }
        }
    }
}
