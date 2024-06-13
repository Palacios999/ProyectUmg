using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ClassLibrary
{
    public class Tablero
    {
        public static void InitGameOne (Avatar avatar) {
            string[,] Matriz;
            int CrystalQty = 6;
            int Portals = 1;
            int Players = 1;
            int TrollsQty = 0;

            switch (avatar.Level)
            {
                case 1:
                    Matriz = new string[3, 3];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar);
                    break;
                case 2:
                    TrollsQty = 1;
                    Matriz = new string[4, 4];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar);
                    break;
                case 3:
                    TrollsQty = 4;
                    Matriz = new string[5, 5];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar);
                    break;
                case 4:
                    TrollsQty = 7;
                    Matriz = new string[6, 6];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar);
                    break;
                case 5:
                    TrollsQty = 12;
                    Matriz = new string[10, 10];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar);
                    break;
            }
        }

        public static void GenerateBoard(ref string[,] matriz, int players, int portals, int crystalQty, int trollsQty, Avatar avatar)
        {
            if (avatar.Level == 1)
            {
                matriz[0, 0] = "&";
                matriz[2, 2] = "O";
                InsertObjectInMatriz(matriz, "#", crystalQty);
            }
            else
            {
                InsertObjectInMatrizNotAdyacent(matriz, "#", crystalQty);
                InsertObjectInMatriz(matriz, "&", players);
                InsertObjectInMatriz(matriz, "O", portals);
                InsertObjectInMatriz(matriz, "¥", trollsQty);
            }
            HandleMovement(matriz, avatar);
        }

        public static void MenuHeader(Avatar avatar)
        {

            WriteLine("-------------------------------------------------------------");
            WriteLine("1. Comandos");
            WriteLine("2. Terminar partida");
            WriteLine("-------------------------------------------------------------");
            WriteLine($"Nombre: {avatar.Name} Nivel: {avatar.Level} Vidas:{avatar.GetTotalLifeJewelry()}");
        }

        public static void HandleMovement(string[,] matriz, Avatar avatar)
        {
            try
            {
                ConsoleKeyInfo dataKey;
                bool gameCompleted = false;
                do
                {
                    var crystalList = findInMatriz(matriz, "#");
                    var trollsList = findInMatriz(matriz, "¥");
                    var portalList = findInMatriz(matriz, "O");
                    var avatarList = findInMatriz(matriz, "&");

                    MenuHeader(avatar);
                    PrintMatriz(matriz);
                    dataKey = ReadKey();
                    if ("ASDWI".Contains(dataKey.KeyChar.ToString().ToUpper()))
                    {
                        switch (dataKey.KeyChar.ToString().ToUpper())
                        {
                            case "A":
                                foreach (var point in avatarList)
                                {
                                    int x = point.Item1;
                                    int y = point.Item2;
                                    y = y-1;
                                    matriz[point.Item1, point.Item2] = null;
                                    matriz[x, y] = null;
                                    matriz[x, y] = "&";
                                }
                                break;
                            case "S":
                                break;
                            case "D":
                                break;
                            case "W":
                                break;
                            case "I":
                                WriteLine("");
                                WriteLine("Presione cualquier tecla para salir del ciclo");
                                return;
                        }
                    } 
                    else
                    {
                        WriteLine("Tecla no válida, por favor use A, S, D, W o I.");
                    }
                } while (!gameCompleted);
            } catch (InvalidOperationException ex) { 
                WriteLine(ex.Message);
            } catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        public static List<(int, int)> findInMatriz(string[,] matriz, string typeObject)
        {
            List<(int, int)> locatePointObject = new List<(int, int)>();

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == typeObject)
                    {
                        locatePointObject.Add((i, j));
                    }
                }
            }

            return locatePointObject;
        }

        //var locatePointObject = findInMatriz(matriz, typeObject);
        //        foreach (var pos in locatePointObject)
        //        {
        //            Console.WriteLine($"{pos.Item1}, {pos.Item2}");
        //        }

        public static void InsertObjectInMatriz(string[,] matriz, string typeObject, int Qty)
        {
            int X, Y;
            int rows = matriz.GetLength(0);
            int cols = matriz.GetLength(1);
            Random random = new Random();

            if (typeObject != null && Qty != 0)
            {
                for (int i = 0; i < Qty; i++)
                {
                    int x, y;
                    do
                    {
                        x = random.Next(0, rows);
                        y = random.Next(0, cols);
                    } while (matriz[x, y] != null);

                    matriz[x, y] = typeObject;
                }
            } else
            {
                WriteLine("Datos no definidos");
            }
        }

        public static void InsertObjectInMatrizNotAdyacent(string[,] matriz, string typeObject, int Qty)
        {
            int rows = matriz.GetLength(0);
            int cols = matriz.GetLength(1);
            Random random = new Random();

            if (typeObject != null && Qty != 0)
            {
                for (int i = 0; i < Qty; i++)
                {
                    int x, y;
                    do
                    {
                        x = random.Next(0, rows);
                        y = random.Next(0, cols);

                    } while (validPosition(matriz, x, y, typeObject) == false);

                    matriz[x, y] = typeObject;
                }
            }
            else
            {
                WriteLine("Datos no definidos");
            }
        }

        private static bool validPosition(string[,] matriz, int x, int y, string typeObject)
        {
            return matriz[x, y] == null && adyacentPoint(matriz, x, y, typeObject) == false;
        }

        private static bool adyacentPoint(string[,] matriz, int x, int y, string typeObject)
        {
            int rows = matriz.GetLength(0);
            int cols = matriz.GetLength(1);

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (Math.Abs(dx) + Math.Abs(dy) != 1) continue; // saltar diagonales

                    int nx = x + dx;
                    int ny = y + dy;
                    if ((nx >= 0 && nx < rows) && (ny >= 0 && ny < cols))
                    {
                        if (matriz[nx, ny] == typeObject)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void PrintMatriz(string[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Write($" {(matriz[i, j] ?? " ").ToString().PadLeft(1)} |");
                }
                WriteLine();
                if (i < matriz.GetLength(0) - 1)
                {
                    WriteLine(new string('-', matriz.GetLength(1) * 5));
                }
            }
        }
    }
}
