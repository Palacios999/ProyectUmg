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
            Clear();
            while (true)
            {
                try
                {
                    WriteLine("Bienvenido a CRYSTAL COLLECTOR");
                    WriteLine("Escoge una opción");
                    WriteLine("1. Iniciar partida");
                    WriteLine("2. Instrucciones");
                    WriteLine("3. Informacion sobre CRYSTAL COLLECTOR");
                    WriteLine("4. Salir del juego");
                    var option = int.Parse(ReadLine());
                    if (option <= 0 || option > 4)
                    {
                        WriteLine("-------------------------------------------------------------");
                        WriteLine("Opción invalida, vuelve a seleccionar una opción");
                        WriteLine("-------------------------------------------------------------");
                    }
                    else
                    {
                        return option;
                    }
                }
                catch (ArgumentNullException ex)
                {
                    WriteLine("-------------------------------------------------------------");
                    WriteLine(ex.Message);
                    WriteLine("-------------------------------------------------------------");
                    return 0;
                }
                catch (Exception ex)
                {
                    WriteLine("-------------------------------------------------------------");
                    WriteLine(ex.Message);
                    WriteLine("-------------------------------------------------------------");
                    return 0;
                }
            }
        }

        public static void PrintInstructions()
        {
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
            ReadKey(true);
        }

        public static void ViewInfoCristal()
        {
            Clear();
            WriteLine("Información sobre CRYSTAL COLLECTOR");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Juego desarrollado por Jonatan Palacios, version de .NET v8.0");
            WriteLine("-------------------------------------------------------------");
            WriteLine("Presiona cualquier tecla para regresar al menu");
            ReadKey(true);
        }

        public static void InitGame()
        {
            Clear();
            try
            {
                int gender;
                string nameAvatar;
                WriteLine("Bienvenido a Crystal Collector");
                WriteLine("Crea tu avatar");
                WriteLine("Escribe el nombre de tu avatar");
                nameAvatar = ReadLine();
                if (nameAvatar == "") {
                    WriteLine("Se detecto que no ah agregado un nombre, se le asignara uno por defecto");
                }
                
                while (true)
                {
                    WriteLine("Escoge el genero de tu avatar");
                    WriteLine("1. Masculino");
                    WriteLine("2. Femenino");
                    gender = int.Parse(ReadLine());
                    if (gender <= 0 || gender > 2)
                    {
                        WriteLine("Genero invalido");
                    } else
                    {
                        break;
                    }
                }

                Avatar avatarPrincipal = null;
                switch(gender)
                {
                    case 1:
                        avatarPrincipal = new Avatar(string.IsNullOrWhiteSpace(nameAvatar) ? "Juan" : nameAvatar, "Masculino");
                        break;
                    case 2:
                        avatarPrincipal = new Avatar(string.IsNullOrWhiteSpace(nameAvatar) ? "Maria" : nameAvatar, "Femenino");
                        break;
                }
                WriteLine("-------------------------------------------------------------");
                WriteLine($"El nombre del avatar es: {avatarPrincipal.Name} y su genero: {avatarPrincipal.Gender}");
                WriteLine("Presiona cualquier tecla para continuar");
                WriteLine("-------------------------------------------------------------");
                ReadKey(true);
                subMenuInit(avatarPrincipal);
            } catch (ArgumentNullException ex) {
                WriteLine(ex.Message);
            } catch (Exception ex) { 
                WriteLine(ex.Message);
            }
        }

        public static void subMenuInit(Avatar avatarPrincipal)
        {
            while (true)
            {
                try
                {
                    Clear();
                    WriteLine("-------------------------------------------------------------");
                    WriteLine("1. Comandos");
                    WriteLine("2. Imprimir tablero");
                    WriteLine("3. Estado de avatar");
                    WriteLine("4. Terminar partida");
                    WriteLine("-------------------------------------------------------------");
                    var OpcionSelect = int.Parse(ReadLine());

                    if(OpcionSelect <= 0 || OpcionSelect > 4) {
                        WriteLine("-------------------------------------------------------------");
                        WriteLine("Opcion no disponible");
                        WriteLine("Presiona cualquier tecla para continuar");
                        WriteLine("-------------------------------------------------------------");
                        ReadKey(true);
                    }

                    switch (OpcionSelect)
                    {
                        case 1:
                            Commands();
                            break;
                        case 2:
                            Tablero.InitGameOne(avatarPrincipal);
                            ReadKey(true);
                            break;
                        case 3:
                            StateAvatar(avatarPrincipal);
                            break;
                        case 4:
                            return;
                    }
                } catch (ArgumentNullException ex)
                {
                    WriteLine(ex.Message);
                    WriteLine("Presiona cualquier tecla para continuar");
                    WriteLine("-------------------------------------------------------------");
                    ReadKey(true);
                } catch (Exception ex)
                {
                    WriteLine(ex.Message);
                    WriteLine("Presiona cualquier tecla para continuar");
                    WriteLine("-------------------------------------------------------------");
                    ReadKey(true);
                }
            }

        }

        public static void Commands()
        {
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
            ReadKey(true);
        }

        public static void StateAvatar(Avatar avatarPrincipal)
        {
            Clear();
            if (avatarPrincipal != null)
            {
                WriteLine("-------------------------------------------------------------");
                WriteLine($"Nombre del avatar: {avatarPrincipal.Name}");
                WriteLine($"Genero del avatar: {avatarPrincipal.Gender}");
                WriteLine($"Joyas de vida: {avatarPrincipal.GetTotalLifeJewelry()}");
                WriteLine($"Cristales recolectados: {avatarPrincipal.GetTotalCrystals()}");
                WriteLine($"Puntos: {avatarPrincipal.GetTotalPoints()}");
                WriteLine($"Ubicación Actual: {avatarPrincipal.GetCurrentCoordinates()}");
                WriteLine("-------------------------------------------------------------");
                WriteLine("Presiona cualquier tecla para continuar");
                ReadKey(true);
            }
        }
    }
}
