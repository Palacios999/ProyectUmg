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
        public static void InitGameOne (Avatar avatar, CollectionBox collectionBox) {
            string[,] Matriz;
            int CrystalQty = 6;
            int Portals = 1;
            int Players = 1;
            int TrollsQty = 0;

            switch (avatar.Level)
            {
                case 1:
                    Matriz = new string[3, 3];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar, collectionBox);
                    break;
                case 2:
                    TrollsQty = 1;
                    Matriz = new string[4, 4];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar, collectionBox);
                    break;
                case 3:
                    TrollsQty = 4;
                    Matriz = new string[5, 5];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar, collectionBox);
                    break;
                case 4:
                    TrollsQty = 7;
                    Matriz = new string[6, 6];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar, collectionBox);
                    break;
                case 5:
                    TrollsQty = 12;
                    Matriz = new string[10, 10];
                    GenerateBoard(ref Matriz, Players, Portals, CrystalQty, TrollsQty, avatar, collectionBox);
                    break;
            }
        }

        public static void GenerateBoard(ref string[,] matriz, int players, int portals, int crystalQty, int trollsQty, Avatar avatar, CollectionBox collectionBox)
        {
            if (avatar.Level == 1)
            {
                matriz[0, 0] = "&";
                matriz[2, 2] = "O";
                InsertObjectInMatriz(matriz, "#", crystalQty);
                updatePositionAvatarGenerate(matriz, avatar);
            }
            else
            {
                InsertObjectInMatrizNotAdyacent(matriz, "#", crystalQty);
                InsertObjectInMatriz(matriz, "&", players);
                InsertObjectInMatriz(matriz, "O", portals);
                InsertObjectInMatriz(matriz, " ", trollsQty);
                updatePositionAvatarGenerate(matriz, avatar);
            }
            
            HandleMovement(matriz, avatar, collectionBox);
        }

        public static void MenuHeader(Avatar avatar, CollectionBox collectionBox)
        {
            var coordinate = avatar.GetCurrentCoordinates();
            var coordinateX = coordinate.Item1;
            var coordinateY = coordinate.Item2;
            WriteLine("-------------------------------------------------------------");
            WriteLine("1. Comandos");
            WriteLine("2. Terminar partida");
            WriteLine("-------------------------------------------------------------");
            WriteLine($"Name: {avatar.Name} | Level: {avatar.Level} | LifeJewel: {collectionBox.GetTotalLifeJewelry()} | Position: ({coordinateX},{coordinateY}) |");
            WriteLine("-------------------------------------------------------------");
        }

        public static bool gameCompleted = false;
        public static void HandleMovement(string[,] matriz, Avatar avatar, CollectionBox collectionBox)
        {
            try
            {
                ConsoleKeyInfo dataKey;
                int level = avatar.Level;
                do
                {
                    var avatarList = findInMatriz(matriz, "&");
                    Clear();
                    MenuHeader(avatar, collectionBox);
                    PrintMatriz(matriz);
                    dataKey = ReadKey(true);
                    if ("ASDWI".Contains(dataKey.KeyChar.ToString().ToUpper()))
                    {
                        switch (dataKey.KeyChar.ToString().ToUpper())
                        {
                            case "A":
                                DetectMove("A", matriz, avatarList, avatar, collectionBox);
                                break;
                            case "S":
                                DetectMove("S", matriz, avatarList, avatar, collectionBox);
                                break;
                            case "D":
                                DetectMove("D", matriz, avatarList, avatar, collectionBox);
                                break;
                            case "W":
                                DetectMove("W", matriz, avatarList, avatar, collectionBox);
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
                } while (!gameCompleted && level == avatar.Level);
            } catch (InvalidOperationException ex) { 
                WriteLine(ex.Message);
            } catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        public static void DetectMove(string keySelect, string[,] matriz, List<(int, int)> avatarPoints, Avatar avatar, CollectionBox collectionBox)
        {
            foreach (var point in avatarPoints)
            {
                int fila = point.Item1;
                int columna = point.Item2;
                switch (keySelect)
                {
                    case "A":
                        columna = columna - 1;
                        break;
                    case "D":
                        columna = columna + 1;
                        break;
                    case "W":
                        fila = fila - 1;
                        break;
                    case "S":
                        fila = fila + 1;
                        break;
                }
                try
                {
                    matriz[point.Item1, point.Item2] = null;
                    if (matriz[fila, columna] != null)
                    {
                        string checkPosition = matriz[fila, columna];
                        switch (checkPosition)
                        {
                            case "#":
                                collectionBox.TotalCrystals += 1;
                                collectionBox.TotalPoints += 15;
                                updateAvatarCoordinate(avatar, matriz, fila, columna);
                                break;
                            case " ":
                                var triviaResult = Trivia.ShowTrivia(avatar, collectionBox);
                                if(triviaResult == true)
                                {
                                    collectionBox.TotalLifeJewelry += 1;
                                    matriz[fila, columna] = null;
                                    updateAvatarCoordinate(avatar, matriz, fila, columna);
                                    InsertObjectInMatriz(matriz, " ", 1);

                                } else
                                {
                                    collectionBox.TotalLifeJewelry += -1;
                                    avatar.Level += -1;
                                    InitGameOne(avatar, collectionBox);
                                }
                              break;
                            case "O":
                                if (avatar.Level == 1) {
                                    var remainingCrystals = findInMatriz(matriz, "#");
                                    if (remainingCrystals.Count == 0)
                                    {
                                        avatar.Level += 1;
                                        InitGameOne(avatar, collectionBox);
                                    } else
                                    {
                                        WriteLine("Recolecta todos los cristales para poder ingresar al portal");
                                        updateAvatarCoordinate(avatar, matriz, point.Item1, point.Item2);
                                        ReadKey(true);
                                    }
                                } else {
                                    var remainingCrystals = findInMatriz(matriz, "#");
                                    if (remainingCrystals.Count == 0)
                                    {
                                        if (avatar.Level == 5)
                                        {
                                            WriteLine("Felicitaciones, has logrado completar el juego");
                                            WriteLine("Si quieres jugar de nuevo ingresa 1 si no presiona cualquier tecla");
                                            string salir = ReadLine();
                                            if (salir.Equals("1"))
                                            {
                                                avatar.Level = 1;
                                                InitGameOne(avatar, collectionBox);
                                            }
                                            else
                                            {
                                                Environment.Exit(0);
                                            }
                                        }
                                        else
                                        {
                                            avatar.Level += 1;
                                            InitGameOne(avatar, collectionBox);
                                        }
                                    }
                                    else
                                    {
                                        var portalPosition = findInMatriz(matriz, "O");
                                        foreach (var portal in portalPosition)
                                        {
                                            matriz[portal.Item1, portal.Item2] = null;
                                        }
                                        updateAvatarCoordinate(avatar, matriz, fila, columna);
                                        InsertObjectInMatriz(matriz, "O", 1);
                                    }
                                }
                                
                                break;
                        }
                    }
                    else
                    {
                        updateAvatarCoordinate(avatar, matriz, fila, columna);
                    }
                }
                catch
                {
                    WriteLine("Te caiste del mapa, regresas un nivel, ten cuidado");
                    ReadKey(true);
                    if (avatar.Level == 1)
                    {
                        InitGameOne(avatar, collectionBox);
                    }
                    else
                    {
                        avatar.Level -= 1;
                        InitGameOne(avatar, collectionBox);
                    }
                }
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

        public static void updatePositionAvatarGenerate(string[,] matriz, Avatar avatar)
        {
            var avatarList = findInMatriz(matriz, "&");
            if (avatarList != null)
            {
                foreach (var option in avatarList)
                {
                    var oneX = option.Item1;
                    var oneY = option.Item2;
                    avatar.UpdateCoordinate(oneX, oneY);
                }
            }
        }

        public static void updateAvatarCoordinate(Avatar avatar, string[,] matriz, int fila, int columna)
        {
            matriz[fila, columna] = "&";
            avatar.UpdateCoordinate(fila, columna);
        }
    }
}
