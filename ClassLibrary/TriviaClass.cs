using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ClassLibrary
{
    public static class Trivia
    {
        public static Boolean ShowTrivia(Avatar avatar, CollectionBox collectionBox)
        {
            WriteLine("");
            WriteLine("-------------------------------------------------------------");
            WriteLine("                 Te has encontrado con un Troll              ");
            WriteLine("                                                             ");
            WriteLine(" -> Si respondes la trivia correctamente ganaras una joya de ");
            WriteLine("    vida, si no regresaras al nivel anterior y perderas una  ");
            WriteLine("    joya de vida.                                            ");
            WriteLine("-------------------------------------------------------------");
            Random random = new Random();
            var numRandom = random.Next(1, 7);
            var resultTrivia = ShowTriviaNumber(numRandom);
            ReadLine();
            return resultTrivia;
        }

        public static Boolean ShowTriviaNumber(int idTrivia)
        {
            try
            {
                switch (idTrivia)
                {
                    case 1:
                        while (true)
                        {
                            WriteLine(@"¿Cuánto es 12 x 12?");
                            WriteLine("1) 124");
                            WriteLine("2) 144");
                            WriteLine("3) 154");
                            WriteLine("4) 164");
                            int optionSelect = int.Parse(ReadLine());
                            if (optionSelect > 4 || optionSelect < 1)
                            {
                                WriteLine(@"Selecciona una opción valida (1-4)");
                                WriteLine("-------------------------------------------------------------");
                            }
                            switch(optionSelect)
                            {
                                case 1:
                                    return false;
                                case 2:
                                    return true;
                                case 3:
                                    return false;
                                case 4:
                                    return false;
                            }
                        }
                    case 2:
                        while (true)
                        {
                            WriteLine(@"¿En qué año se estrenó la película ""Titanic""?");
                            WriteLine("1) 1995");
                            WriteLine("2) 1997");
                            WriteLine("3) 1999");
                            WriteLine("4) 2001");
                            int optionSelect = int.Parse(ReadLine());
                            if (optionSelect > 4 || optionSelect < 1)
                            {
                                WriteLine(@"Selecciona una opción valida (1-4)");
                                WriteLine("-------------------------------------------------------------");
                            }
                            switch (optionSelect)
                            {
                                case 1:
                                    return false;
                                case 2:
                                    return true;
                                case 3:
                                    return false;
                                case 4:
                                    return false;
                            }
                        }
                    case 3:
                        while (true)
                        {
                            WriteLine(@"¿Cuál es la raíz cuadrada de 100?");
                            WriteLine("1) 10");
                            WriteLine("2) 12");
                            WriteLine("3) 14");
                            WriteLine("4) 16");
                            int optionSelect = int.Parse(ReadLine());
                            if (optionSelect > 4 || optionSelect < 1)
                            {
                                WriteLine(@"Selecciona una opción valida (1-4)");
                                WriteLine("-------------------------------------------------------------");
                            }
                            switch (optionSelect)
                            {
                                case 1:
                                    return true;
                                case 2:
                                    return false;
                                case 3:
                                    return false;
                                case 4:
                                    return false;
                            }
                        }
                    case 4:
                        while (true)
                        {
                            WriteLine(@"¿En qué año fue fundada la Universidad Mariano Gálvez de Guatemala?");
                            WriteLine("1) 1966");
                            WriteLine("2) 1969");
                            WriteLine("3) 1972");
                            WriteLine("4) 1975");
                            int optionSelect = int.Parse(ReadLine());
                            if (optionSelect > 4 || optionSelect < 1)
                            {
                                WriteLine(@"Selecciona una opción valida (1-4)");
                                WriteLine("-------------------------------------------------------------");
                            }
                            switch (optionSelect)
                            {
                                case 1:
                                    return false;
                                case 2:
                                    return false;
                                case 3:
                                    return true;
                                case 4:
                                    return false;
                            }
                        }
                    case 5:
                        while (true)
                        {
                            WriteLine(@"¿Cuál es el valor de π (Pi) aproximado a dos decimales?");
                            WriteLine("1) 3.12");
                            WriteLine("2) 3.14");
                            WriteLine("3) 3.16");
                            WriteLine("4) 3.18");
                            int optionSelect = int.Parse(ReadLine());
                            if (optionSelect > 4 || optionSelect < 1)
                            {
                                WriteLine(@"Selecciona una opción valida (1-4)");
                                WriteLine("-------------------------------------------------------------");
                            }
                            switch (optionSelect)
                            {
                                case 1:
                                    return false;
                                case 2:
                                    return true;
                                case 3:
                                    return false;
                                case 4:
                                    return false;
                            }
                        }
                    case 6:
                        while (true)
                        {
                            WriteLine(@"¿Cuál es el lema de la Universidad Mariano Gálvez de Guatemala?");
                            WriteLine(@"1) Por una Guatemala mejor");
                            WriteLine(@"2) Educación con visión");
                            WriteLine(@"3) Saber para servir");
                            WriteLine(@"4) Formación integral para el futuro");
                            int optionSelect = int.Parse(ReadLine());
                            if (optionSelect > 4 || optionSelect < 1)
                            {
                                WriteLine(@"Selecciona una opción valida (1-4)");
                                WriteLine("-------------------------------------------------------------");
                            }
                            switch (optionSelect)
                            {
                                case 1:
                                    return false;
                                case 2:
                                    return false;
                                case 3:
                                    return true;
                                case 4:
                                    return false;
                            }
                        }
                    default:
                        return false;
                }
            } catch (ArgumentNullException ex){
                WriteLine(ex.Message);
                return false;
            } catch (Exception ex)
            {
                WriteLine(ex.Message);
                return false;
            }
       
        }
    }
}
