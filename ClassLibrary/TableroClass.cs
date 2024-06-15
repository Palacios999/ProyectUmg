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
        // Función iniciadora del juego, esta genera los datos que tendra la matriz y la cantidad de objetos que esta tendra.
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

        // Función la cual inserta los datos en la matriz, y al final de esta detecta los movimientos que se realizan por consola.
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

        // Función que muestra el encabeza, este obtiene datos del avatar y del collectionBox
        public static void MenuHeader(Avatar avatar, CollectionBox collectionBox)
        {
            var coordinate = avatar.GetCurrentCoordinates();
            var coordinateX = coordinate.Item1;
            var coordinateY = coordinate.Item2;
            WriteLine("-------------------------------------------------------------");
            WriteLine("1. Regresar al menu");
            WriteLine("-------------------------------------------------------------");
            WriteLine($"Name: {avatar.Name} | Level: {avatar.Level} | LifeJewel: {collectionBox.GetTotalLifeJewelry()} | TotalPoints: {collectionBox.GetTotalPoints()} | Position: ({coordinateX},{coordinateY}) |");
            WriteLine("-------------------------------------------------------------");
        }

        // Variable la cual controlá si el juego ah sido completado
        public static bool gameCompleted = false;

        // Función la cual imprimira el encabezado con los datos, imprimir el tablero y obtener los datos de consola, la cual dependiendo si teclea A, S, D, W ó 1 realizara una función especifica.
        public static void HandleMovement(string[,] matriz, Avatar avatar, CollectionBox collectionBox)
        {
            try
            {
                ConsoleKeyInfo dataKey;
                // El ciclo se repite hasta que ser gane el juego
                do
                {
                    // Obtiene los datos de la posicíón actual del avatar.
                    var avatarList = findInMatriz(matriz, "&");
                    // Limpia la consola
                    Clear();
                    // Muestra el menu del encabezado en la partida
                    MenuHeader(avatar, collectionBox);
                    // Imprime el tablero con los objetos agregados con anterioridad.
                    PrintMatriz(matriz);
                    // Lee el dato declarado con anterioridad para despues validarlo.
                    dataKey = ReadKey(true);
                    if ("ASDW1".Contains(dataKey.KeyChar.ToString().ToUpper()))
                    {
                        // Si la tecla seleccionada es valida este realizara la función DetectMove el cual tiene la logica del movimiento dependiendo la tecla que se obtuvo con la declaración de la variable Unicode.
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
                            case "1":
                                WriteLine("");
                                WriteLine("Presione cualquier tecla para regresar al menu");
                                return;
                        }
                    } 
                    else
                    {
                        WriteLine("Tecla no válida, por favor use A, S, D, W o 1.");
                    }
                } while (!gameCompleted);
            } // Excepción de operación invalida 
            catch (InvalidOperationException ex) { 
                WriteLine(ex.Message);
            } // Excepción general
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        // Función que actualiza la posición del avatar, ademas dependiendo si este se topa con un cristal, troll o portal realizara una función
        public static void DetectMove(string keySelect, string[,] matriz, List<(int, int)> avatarPoints, Avatar avatar, CollectionBox collectionBox)
        {
            // Obtiene los datos de la posición del avatar recorriendo la lista
            foreach (var point in avatarPoints)
            {
                // inserta los datos de la posición en variables distintas.
                int fila = point.Item1;
                int columna = point.Item2;
                // Modifica la coordenada dependiendo la tecla seleccionada.
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
                // Bloque Try Catch para manejo de excepciones.
                try
                {
                    // Obtiene la ubicación del avatar inicial y lo agrega como nulo, esto por que el jugar ya se movio, entonces esto quiere decir que se eliminara y parasara a la casilla a la cual se paso.
                    matriz[point.Item1, point.Item2] = null;

                    // Valida si en la nueva posición hay un objeto, si lo hay recorre la condiciónal si
                    if (matriz[fila, columna] != null)
                    {
                        // Si hay un objeto obtiene el valor del objeto en esa posición.
                        string checkPosition = matriz[fila, columna];

                        // Switch el cual segun el objeto realizara una función.
                        switch (checkPosition)
                        {
                            case "#":
                                collectionBox.TotalCrystals += 1;
                                collectionBox.TotalPoints += 15;
                                // actualiza la coordenada del avatar
                                updateAvatarCoordinate(avatar, matriz, fila, columna);

                                // Si no hay cristales en el tablero, se mostrara el mensaje, ya puedes avanzar al siguiente nivel.
                                var validateCrystals = findInMatriz(matriz, "#");
                                if (validateCrystals.Count == 0)
                                {
                                    WriteLine("");
                                    WriteLine("-------------------------------------------------------------");
                                    WriteLine("Ya puedes avanzar al siguiente nivel, busca el portal");
                                    WriteLine("-------------------------------------------------------------");
                                    // Detiene la ejecución por un segundo para poder ver el mensaje, de ahí desaparece.
                                    Task.Delay(1000).Wait();
                                }
                                break;
                            case " ":
                                // Muestra la trivia y la guarda en una variable
                                var triviaResult = Trivia.ShowTrivia(avatar, collectionBox);
                                // Si se responde bien a la trivia se dan 10 puntos y una vida, ademas se actualiza la ubicación del avatar,
                                // y se genera el troll en otra ubicación
                                if(triviaResult == true)
                                {
                                    collectionBox.TotalLifeJewelry += 1;
                                    collectionBox.TotalPoints += 10;
                                    matriz[fila, columna] = null;
                                    updateAvatarCoordinate(avatar, matriz, fila, columna);
                                    InsertObjectInMatriz(matriz, " ", 1);
                                } // Si responde mal
                                else
                                {
                                    // Se le quitara una vida
                                    collectionBox.TotalLifeJewelry += -1;
                                    // SI ya no tiene vidas es GameOver
                                    if (collectionBox.TotalLifeJewelry == 0)
                                    {
                                        Clear();
                                        WriteLine("-------------------------------------------------------------");
                                        WriteLine("-------------------------GAME OVER---------------------------");
                                        WriteLine("-------------------------------------------------------------");
                                        WriteLine($"Nombre del avatar: {avatar.Name}");
                                        WriteLine($"Genero del avatar: {avatar.Gender}");
                                        WriteLine($"Joyas de vida: {collectionBox.GetTotalLifeJewelry()}");
                                        WriteLine($"Cristales recolectados: {collectionBox.GetTotalCrystals()}");
                                        WriteLine($"Puntos: {collectionBox.GetTotalPoints()}");
                                        WriteLine($"Ubicación Actual: {avatar.GetCurrentCoordinates()}");
                                        WriteLine("-------------------------------------------------------------");
                                        avatar.ResetDataAvatar();
                                        collectionBox.ResetCollectionBox();
                                        WriteLine("Presiona cualquier tecla para continuar");
                                        gameCompleted = true;
                                    } // Si aun tiene vidas pero responde mal, se le quitara 10 pts y perdera una vida. y iniciara el nivel anterior.
                                    else
                                    {
                                        collectionBox.TotalPoints += -10;
                                        avatar.Level += -1;
                                        InitGameOne(avatar, collectionBox);
                                    }
                                }
                              break;
                            case "O":
                                if (avatar.Level == 1)
                                {
                                    // Cuenta el total de cristales en el tablero
                                    var remainingCrystals = findInMatriz(matriz, "#");
                                    // Si ya no hay cristales en el tablero
                                    if (remainingCrystals.Count == 0)
                                    {
                                        //Se gana una vida, se suman 50pts y se pasa al siguiente nivel.
                                        avatar.Level += 1;
                                        collectionBox.TotalPoints += 50;
                                        InitGameOne(avatar, collectionBox);
                                    }
                                    else
                                    {
                                        // Resta 5 pts por intentar entrar al portal sin los cristales necesarios.
                                        collectionBox.TotalPoints -= 5;
                                        WriteLine("Recolecta todos los cristales para poder ingresar al portal");
                                        // Se queda en la ubicación antes de moverse
                                        updateAvatarCoordinate(avatar, matriz, point.Item1, point.Item2);
                                        ReadKey(true);
                                    }
                                }
                                else
                                {
                                    // Se cuenta la cantidad de cristales
                                    var remainingCrystals = findInMatriz(matriz, "#");
                                    if (remainingCrystals.Count == 0)
                                    {
                                        // Si ya no hay cristales y si el avatar es nivel 5, eso quiere decir que gano
                                        if (avatar.Level == 5)
                                        {
                                            // Suma los puntos, muestra el estado, y le da la opción de jugar otra vez
                                            collectionBox.TotalPoints += 1000;
                                            WriteLine("-------------------------------------------------------------");
                                            WriteLine($"Felicitaciones {avatar.Name}, has logrado completar el juego");
                                            WriteLine("                                                             ");
                                            WriteLine(" -> Haz obtenido                                             ");
                                            WriteLine("-------------------------------------------------------------");
                                            WriteLine($"Joyas de vida: {collectionBox.GetTotalLifeJewelry()}");
                                            WriteLine($"Cristales recolectados: {collectionBox.GetTotalCrystals()}");
                                            WriteLine($"Puntos: {collectionBox.GetTotalPoints()}");
                                            WriteLine("-------------------------------------------------------------");
                                            // Si quiere jugar otra vez tiene que seleccionar 1
                                            WriteLine("Si quieres jugar de nuevo ingresa 1 si no presiona cualquier tecla");
                                            string salir = ReadLine();
                                            if (salir.Equals("1"))
                                            {
                                                avatar.ResetDataAvatar();
                                                collectionBox.ResetCollectionBox();
                                                InitGameOne(avatar, collectionBox);
                                            } // Si no sale del juego.
                                            else
                                            {
                                                Environment.Exit(0);
                                            }
                                        }
                                        else
                                        {
                                            // Si no es nivel 5, sube de nivel, suma los puntos y genera el tablero del siguiente nivel
                                            avatar.Level += 1;
                                            collectionBox.TotalPoints += 50;
                                            InitGameOne(avatar, collectionBox);
                                        }
                                    }
                                    else
                                    {
                                        // Si no tiene los cristales del portal, busca el portal
                                        var portalPosition = findInMatriz(matriz, "O");
                                        foreach (var portal in portalPosition)
                                        {
                                            // Obtiene la posición del portal y la deja como nula
                                            matriz[portal.Item1, portal.Item2] = null;
                                        }
                                        // Se restan 5 puntos
                                        collectionBox.TotalPoints -= 5;
                                        // Se actualiza el avatar a la posición del avatar
                                        updateAvatarCoordinate(avatar, matriz, fila, columna);
                                        // El portal se genera en una posición random.
                                        InsertObjectInMatriz(matriz, "O", 1);
                                    }
                                }
                                break;
                        }
                    } // Si no hay nada en la posición simplemente actualiza su ubicación en la matriz.
                    else
                    {
                        updateAvatarCoordinate(avatar, matriz, fila, columna);
                    }
                }
                catch
                {
                    // El catch se ejecutara cuando se intente actualizar el avatar a una posición fuera de la matriz, por lo que se integro la funcionalidad como si se callera del tablero,
                    // Esto restara un nivel, quitara una vida, y restara los puntos del nivel alcanzado.
                    WriteLine("Te caiste del mapa, regresas un nivel, ten cuidado");
                    ReadKey(true);
                    if (avatar.Level == 1)
                    {
                        InitGameOne(avatar, collectionBox);
                    }
                    else
                    {
                        collectionBox.TotalPoints -= 50;
                        avatar.Level -= 1;
                        collectionBox.TotalLifeJewelry -= 1;
                        // Iniciara el juego nuevamente
                        InitGameOne(avatar, collectionBox);
                    }
                }
            }
        }

        // Función para buscar la posición del objeto especificado, este devolvera una lista en dado caso haya mas de uno en el tablero.
        public static List<(int, int)> findInMatriz(string[,] matriz, string typeObject)
        {
            List<(int, int)> pointLocation = new List<(int, int)>();

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == typeObject)
                    {
                        pointLocation.Add((i, j));
                    }
                }
            }

            return pointLocation;
        }

        // Función para agregar datos a la matriz, este obtiene la matriz y el objeto a insertar y la cantidad de este objeto,
        // este tambien genera posiciones random.
        public static void InsertObjectInMatriz(string[,] matriz, string typeObject, int Qty)
        {
            int X, Y;
            int rows = matriz.GetLength(0);
            int cols = matriz.GetLength(1);
            Random random = new Random();

            // Valida que se hayan agregado los datos a la función
            if (typeObject != null && Qty != 0 && matriz != null)
            {
                // Ciclo para ubicar el objeto la cantidad de veces que se proporciono en la función
                for (int i = 0; i < Qty; i++)
                {
                    // Variables de la ubicación del objeto
                    int x, y;

                    // Ciclo el cual valida si hay un objeto existente, si la posición generada es distinta a undefined, es decir
                    // si ya tiene un objeto agregado, este vuelve a generar otra posición.
                    do
                    {
                        x = random.Next(0, rows);
                        y = random.Next(0, cols);
                    } while (matriz[x, y] != null);

                    // Al salir del ciclo con una posición valida, se inserta el objeto en la posición de la matriz disponible.
                    matriz[x, y] = typeObject;
                }
            } // Else si no se agregan los datos en la función inicial.
            else
            {
                WriteLine("Datos no definidos");
            }
        }

        // Inserta los objetos definidos en la función de forma en que las posiciones no sean contiguas, para ser especifico
        // Arriba, abajo, derecha e izquierda, y la ubicación generada  de la ubicación 
        public static void InsertObjectInMatrizNotAdyacent(string[,] matriz, string typeObject, int Qty)
        {
            int rows = matriz.GetLength(0);
            int cols = matriz.GetLength(1);
            Random random = new Random();

            // Valida que los datos agregados a la función no sean nulos
            if (typeObject != null && Qty != 0 && matriz != null)
            {
                // Ciclo de la cantidad de objetos que se mandan.
                for (int i = 0; i < Qty; i++)
                {
                    int x, y;
                    // Se agrego el ciclo, para validar si el punto es adyacente y la posición a tomar es nula, esto retornara verdadero
                    // si no volvera a generar otro punto.
                    do
                    {
                        x = random.Next(0, rows);
                        y = random.Next(0, cols);

                    } while (validPosition(matriz, x, y, typeObject) == false);

                    // agrega el objeto en la posición generada.
                    matriz[x, y] = typeObject;
                }
            } // Si los datos son nulos se devuelve un WriteLine.
            else
            {
                WriteLine("Datos no definidos");
            }
        }

        // Valida si el punto es adyacente y la posición es nula
        private static bool validPosition(string[,] matriz, int x, int y, string typeObject)
        {
            return matriz[x, y] == null && adyacentPoint(matriz, x, y, typeObject) == false;
        }

        // Validación si el punto es adyacente
        private static bool adyacentPoint(string[,] matriz, int x, int y, string typeObject)
        {
            int rows = matriz.GetLength(0);
            int cols = matriz.GetLength(1);

            // Ciclo el cual valida alrededor del punto generado en un area 3x3 
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    // Si el valor de los puntos es distinto a 1 esto quiere decir que la posición
                    // es una ubicación en cruz o la central, por lo que continuara con el siguiente
                    // valor del ciclo, hasta encontrar posiciones validas.
                    if (Math.Abs(dx) + Math.Abs(dy) != 1) continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    // Valida se la posición no sobrepasa los limites de la matriz o tablero
                    if ((nx >= 0 && nx < rows) && (ny >= 0 && ny < cols))
                    {
                        // valida si la coordenada generada es igual a un objeto identico
                        if (matriz[nx, ny] == typeObject)
                        {
                            return true;
                        }
                    }
                }
            }
            // Si no se devuelve true, este retornara false que significa que la posición no es adyacente al punto agregado en la función.
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

        // Actualiza la posición del avatar en donde se encuentra en la matriz, en la clase avatar
        // para obtener la posición en tiempo real del avatar.
        public static void updatePositionAvatarGenerate(string[,] matriz, Avatar avatar)
        {
            // Busca la posición del avatar           
            var avatarList = findInMatriz(matriz, "&");
            if (avatarList != null)
            {
                // recorre la lista de posicíones del avatar.
                foreach (var option in avatarList)
                {
                    var oneX = option.Item1;
                    var oneY = option.Item2;
                    // Actualiza la ubicación del avatar.
                    avatar.UpdateCoordinate(oneX, oneY);
                }
            }
        }

        public static void updateAvatarCoordinate(Avatar avatar, string[,] matriz, int fila, int columna)
        {
            // obtiene la posición actual y agrega al avatar
            matriz[fila, columna] = "&";
            // Actualiza la posición del avatar
            avatar.UpdateCoordinate(fila, columna);
        }
    }
}
