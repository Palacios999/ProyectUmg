using static System.Console;

namespace ProyectoFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicialización de variables
            bool showMenu = true;
            int optionMenu = 0;

            // Ciclo While(true) - Este se detiene hasta que retorne un valor.
            while (true)
            {
                // Condicional por medio de bool para saber si se mostrara el menu al inicio, el cual es verdadero.
                if (showMenu)
                {
                    // Función retorna opción del menu de la función FirtMenu ubicada en la clase MenusModule
                    optionMenu = MenusModule.FirstMenu();
                }

                // Switch el cual ejecuta una función especifica dependiendo del valor de opcionMenu.
                switch (optionMenu)
                {
                    case 1:
                        // Función para inicializar el juego
                        MenusModule.InitGame();
                        break;
                    case 2:
                        // Función que muestra las instrucciones del juego
                        MenusModule.PrintInstructions();
                        // Luego de mostrar las instrucciones vuelve a mostrar FirtMenu
                        showMenu = true;
                        break;
                    case 3:
                        // Función que muestra la información del juego
                        MenusModule.ViewInfoCristal();
                        // Luego de mostrar la información vuelve a mostrar FirtMenu
                        showMenu = true;
                        break;
                    case 4:
                        // Finaliza la ejecución del ciclo, la cual a su vez finaliza la ejecución de la aplicación por consola.
                        WriteLine("¡Hasta luego!");
                        return;
                    default:
                        // Si se devuelve un valor no predeterminado se vuelve a repetir el ciclo, este comenzara mostrando el menu por defecto.
                        WriteLine("Elije una opcion correcta");
                        ReadKey();
                        break;
                }
            }
        }
    }
}
