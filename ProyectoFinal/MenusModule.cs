using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProyectoFinal
{
    public static class MenusModule
    {
        public static int FirstMenu()
        {
            // Se limpia la consola para tener un área limpia.
            Clear();
            // Se agrega un ciclo while(true) para que se repita el menu hazta que se retorne un valor valido.
            while (true)
            {
                // Se maneja try para el control de excepciones no controladas.
                try
                {
                    WriteLine("Bienvenido a CRYSTAL COLLECTOR");
                    WriteLine("Escoge una opción");
                    WriteLine("1. Iniciar partida");
                    WriteLine("2. Instrucciones");
                    WriteLine("3. Informacion sobre CRYSTAL COLLECTOR");
                    WriteLine("4. Salir del juego");
                    // Se lee la información obtenida por el teclado
                    var option = int.Parse(ReadLine());
                    // Se valida que este en el rango de opciones a escoger, de no ser asi muestra un mensaje personalizado
                    if (option <= 0 || option > 4)
                    {
                        WriteLine("-------------------------------------------------------------");
                        WriteLine("Opción invalida, vuelve a seleccionar una opción");
                        WriteLine("-------------------------------------------------------------");
                    }
                    // si es una opción valida se retorna el valor del mismo.
                    else
                    {
                        return option;
                    }
                } // Se maneja la excepcion si hay un dato nulo
                catch (ArgumentNullException ex)
                {
                    WriteLine("-------------------------------------------------------------");
                    WriteLine(ex.Message);
                    WriteLine("-------------------------------------------------------------");
                    // Se retorna un valor no valido en el switch en donde se mandara al menu para que vuelva a repetir el ciclo.
                    return 0;
                } // Se maneja la excepcion para errores generales
                catch (Exception ex)
                {
                    WriteLine("-------------------------------------------------------------");
                    WriteLine(ex.Message);
                    WriteLine("-------------------------------------------------------------");
                    // Se retorna un valor no valido en el switch en donde se mandara al menu para que vuelva a repetir el ciclo.
                    return 0;
                }
            }
        }

        // Función para imprimir datos en consola
        public static void PrintInstructions()
        {
            // Se limpia la consola para tener un área limpia.
            Clear();
            WriteLine("////////////////////////////////////////////////////////////////////////////////////////////");
            WriteLine("-------------------------------------- Instrucciones ---------------------------------------");
            WriteLine("////////////////////////////////////////////////////////////////////////////////////////////");
            WriteLine("");
            WriteLine("El juego consiste en un Avatar que debe de moverse a lo largo de un tablero,");
            WriteLine("el cual podrá cambiar dependiendo el nivel que se encuentre jugando.");
            WriteLine("");
            WriteLine("El objetivo principal del juego es recolectar cristales que se encuentran");
            WriteLine("esparcidos por el tablero; el Avatar podrá subir de nivel si y sólo si");
            WriteLine("ha recolectado todos los cristales de cada tablero.");
            WriteLine("Dentro del tablero se encontrarán con varios obstáculos que pondrán a prueba a dicho Avatar.");
            WriteLine("");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Espero disfrutes el juego");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Presiona cualquier tecla para regresar al menu");
            // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
            // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
            ReadKey(true);
        }

        public static void ViewInfoCristal()
        {
            // Se limpia la consola para tener un área limpia.
            Clear();
            WriteLine("Información sobre CRYSTAL COLLECTOR");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Juego desarrollado por Jonatan Palacios, version de .NET v8.0");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Presiona cualquier tecla para regresar al menu");
            // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
            // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
            ReadKey(true);
        }

        public static void InitGame()
        {
            // Se limpia la consola para tener un área limpia.
            Clear();
            // Se agrega try catch para el manejo de excepciones
            try
            {
                // Declaración de variables
                int gender;
                string nameAvatar;
                WriteLine("Bienvenido a Crystal Collector");
                WriteLine("Crea tu avatar");
                WriteLine("Escribe el nombre de tu avatar");
                // agrega datos de lo escrito en consola a la variable
                nameAvatar = ReadLine();

                // Condicional que detecta si no se agrego un valor a la variable nameAvatar
                if (nameAvatar == "") {
                    WriteLine("Se detecto que no ah agregado un nombre, se le asignara uno por defecto");
                }
                
                // Ciclo while (true)
                while (true)
                {
                    WriteLine("Escoge el genero de tu avatar");
                    WriteLine("1. Masculino");
                    WriteLine("2. Femenino");

                    // variable a la cual se le va a agregar el valor que se obtenga por consola
                    gender = int.Parse(ReadLine());
                    if (gender <= 0 || gender > 2)
                    {
                        WriteLine("Genero invalido");
                    } else
                    {
                        // finaliza el ciclo para volver a iniciar
                        break;
                    }
                }

                // Inicialización de constructores de clase
                Avatar avatarPrincipal = null;
                CollectionBox collectionBox = null;

                // switch para saber que genero se escogio, dependiendo es esto se creara el vatar con los datos predeterminados.
                switch(gender)
                {
                    case 1:
                        avatarPrincipal = new Avatar(string.IsNullOrWhiteSpace(nameAvatar) ? "Juan" : nameAvatar, "Masculino");
                        collectionBox = new CollectionBox(0, 0, 3);
                        break;
                    case 2:
                        avatarPrincipal = new Avatar(string.IsNullOrWhiteSpace(nameAvatar) ? "Maria" : nameAvatar, "Femenino");
                        collectionBox = new CollectionBox(0, 0, 3);
                        break;
                }
                WriteLine("-------------------------------------------------------------");
                WriteLine($"El nombre del avatar es: {avatarPrincipal.Name} y su genero: {avatarPrincipal.Gender}");
                WriteLine("Presiona cualquier tecla para continuar");
                WriteLine("-------------------------------------------------------------");
                // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
                // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
                ReadKey(true);

                subMenuInit(avatarPrincipal, collectionBox);
            } // Excepción de campo nulo
            catch (ArgumentNullException ex) {
                WriteLine(ex.Message);
            } // Excepción generales
            catch (Exception ex) { 
                WriteLine(ex.Message);
            }
        }

        public static void subMenuInit(Avatar avatarPrincipal, CollectionBox collectionBox)
        {
            // Ciclo while (true)
            while (true)
            {
                // Se agrega try catch para el manejo de excepciones
                try
                {
                    // Se limpia la consola para tener un área limpia.
                    Clear();
                    WriteLine("-------------------------------------------------------------");
                    WriteLine("1. Comandos");
                    WriteLine("2. Imprimir tablero");
                    WriteLine("3. Estado de avatar");
                    WriteLine("4. Terminar partida");
                    WriteLine("-------------------------------------------------------------");
                    // Leer dato en consola 
                    var OpcionSelect = int.Parse(ReadLine());

                    // Condición de opciones
                    if(OpcionSelect <= 0 || OpcionSelect > 4) {
                        WriteLine("-------------------------------------------------------------");
                        WriteLine("Opcion no disponible");
                        WriteLine("Presiona cualquier tecla para continuar");
                        WriteLine("-------------------------------------------------------------");
                        // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
                        // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
                        ReadKey(true);
                    }

                    // switch para identificar que función iniciar
                    switch (OpcionSelect)
                    {
                        case 1:
                            Commands();
                            break;
                        case 2:
                            Tablero.InitGameOne(avatarPrincipal, collectionBox);
                            ReadKey(true);
                            break;
                        case 3:
                            StateAvatar(avatarPrincipal, collectionBox);
                            break;
                        case 4:
                            avatarPrincipal.ResetDataAvatar();
                            collectionBox.ResetCollectionBox();
                            return;
                    }
                } // Excepcion cuando se recibe un argumento nulo
                catch (ArgumentNullException ex)
                {
                    WriteLine(ex.Message);
                    WriteLine("Presiona cualquier tecla para continuar");
                    WriteLine("-------------------------------------------------------------");
                    // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
                    // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
                    ReadKey(true);

                } // Excepciones generales
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                    WriteLine("Presiona cualquier tecla para continuar");
                    WriteLine("-------------------------------------------------------------");
                    // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
                    // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
                    ReadKey(true);
                }
            }

        }

        public static void Commands()
        {
            // Se limpia la consola para tener un área limpia.
            Clear();
            WriteLine("-------------------------------------------------------------");
            WriteLine("Al iniciar el juego tienes que usar los siguientes comandos:");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Para moverte por el tablero tienes que usar las teclas A, S, D, W.");
            WriteLine("A - Para moverte hacia la derecha");
            WriteLine("S - Para moverte a la izquierda");
            WriteLine("D - Para moverte hacia abajo");
            WriteLine("W - Para moverte hacia arriba");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Presiona cualquier tecla para continuar");
            // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
            // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
            ReadKey(true);
        }

        public static void StateAvatar(Avatar avatarPrincipal, CollectionBox collectionBox)
        {
            // Se limpia la consola para tener un área limpia.
            Clear();
            // condicional si el avatar aun no se a creado.
            if (avatarPrincipal != null)
            {
                WriteLine("-------------------------------------------------------------");
                WriteLine($"Nombre del avatar: {avatarPrincipal.Name}");
                WriteLine($"Genero del avatar: {avatarPrincipal.Gender}");
                WriteLine($"Joyas de vida: {collectionBox.GetTotalLifeJewelry()}");
                WriteLine($"Cristales recolectados: {collectionBox.GetTotalCrystals()}");
                WriteLine($"Puntos: {collectionBox.GetTotalPoints()}");
                WriteLine($"Ubicación Actual: {avatarPrincipal.GetCurrentCoordinates()}");
                WriteLine("-------------------------------------------------------------");
                WriteLine("Presiona cualquier tecla para continuar");
                // Como la ejecución es instantanea, se necesita de un elemento el cual espere un evento,
                // para mantener el mensaje, al seleccionar cualquier boton se terminara la ejecución de dicha función
                ReadKey(true);
            }
        }
    }
}
