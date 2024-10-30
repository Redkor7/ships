using System.Numerics;
using System.Text;

namespace BattleShip

{
    internal class Program
    {
        static void DrowField(char[,] field, int curPlayer)
        {
            int startSdv = 0;
            if (curPlayer == 2)
                startSdv = 45;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(5 + startSdv, 0);
            Console.WriteLine("A   B   C   D   E   F   G   H   I   J");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(4 + startSdv, 1);
            Console.WriteLine("--- --- --- --- --- --- --- --- --- ---");
            int sdvig = 0;
            int sdvigv = 0;
            for (int i = 0; i < 10; i++)
            {
                sdvig = 0;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(1 + startSdv, 2 + sdvigv);
                Console.Write(i + 1);
                Console.ForegroundColor = ConsoleColor.Cyan;

                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == ' ')
                    {
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        if (j == 0)
                        {
                            Console.Write("| ");
                            sdvig += 2;
                        }
                        else
                        {
                            Console.Write(" | ");
                            sdvig += 3;
                        }
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        Console.Write($"{field[i, j]}");
                        sdvig++;
                    }
                    else if (field[i, j] == 'X')
                    {
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        if (j == 0)
                        {
                            Console.Write("| ");
                            sdvig += 2;
                        }
                        else
                        {
                            Console.Write(" | ");
                            sdvig += 3;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        Console.Write($"{field[i, j]}");
                        sdvig++;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (field[i, j] == '.')
                    {
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        if (j == 0)
                        {
                            Console.Write("| ");
                            sdvig += 2;
                        }
                        else
                        {
                            Console.Write(" | ");
                            sdvig += 3;
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        Console.Write($"{field[i, j]}");
                        sdvig++;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (field[i, j] == 'S')
                    {
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        if (j == 0)
                        {
                            Console.Write("| ");
                            sdvig += 2;
                        }
                        else
                        {
                            Console.Write(" | ");
                            sdvig += 3;
                        }
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                        Console.Write($"{field[i, j]}");
                        sdvig++;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                }
                Console.SetCursorPosition(3 + sdvig + startSdv, 2 + sdvigv);
                Console.Write(" |");
                sdvigv++;
                Console.SetCursorPosition(4 + startSdv, 2 + sdvigv);
                Console.WriteLine("--- --- --- --- --- --- --- --- --- ---");
                sdvigv++;
            }
        }

        //Парсинг ходов
        static int[] Parsing(string s)
        {
            int iCur = -1, jCur = -1;
            switch (s[0])
            {
                case 'A':
                    jCur = 0;
                    break;
                case 'B':
                    jCur = 1;
                    break;
                case 'C':
                    jCur = 2;
                    break;
                case 'D':
                    jCur = 3;
                    break;
                case 'E':
                    jCur = 4;
                    break;
                case 'F':
                    jCur = 5;
                    break;
                case 'G':
                    jCur = 6;
                    break;
                case 'H':
                    jCur = 7;
                    break;
                case 'I':
                    jCur = 8;
                    break;
                case 'J':
                    jCur = 9;
                    break;
            }
            switch (s[1])
            {
                case '1':
                    if (s.Length > 2)
                    {
                        if (s[2] == '0')
                        {
                            iCur = 9;
                            break;
                        }
                        else
                            break;
                    }
                    else
                    {
                        iCur = 0;
                        break;
                    }
                case '2':
                    iCur = 1;
                    break;
                case '3':
                    iCur = 2;
                    break;
                case '4':
                    iCur = 3;
                    break;
                case '5':
                    iCur = 4;
                    break;
                case '6':
                    iCur = 5;
                    break;
                case '7':
                    iCur = 6;
                    break;
                case '8':
                    iCur = 7;
                    break;
                case '9':
                    iCur = 8;
                    break;
            }
            return new int[] { iCur, jCur };
        }

        // функция расстановки кораблей
        static char[,] Preparing(char[,] field, int curPlayer)
        {
            List<int> iCurs = new List<int>();
            List<int> jCurs = new List<int>();
            bool canPlace = false;
            StringBuilder koord1 = new StringBuilder();
            StringBuilder koord2 = new StringBuilder();
            StringBuilder koord3 = new StringBuilder();
            StringBuilder koord4 = new StringBuilder();
            Console.Clear();
            DrowField(field, curPlayer);
            string s;

            // расстановка однопалубных клраблей
            for (int i = 0; i < 4; i++)
            {
                canPlace = false;
                while (!canPlace)
                {
                    iCurs.Clear();
                    jCurs.Clear();
                    if (curPlayer == 1)
                    {
                        Console.SetCursorPosition(50, 5);
                        Console.Write("Поставьте однопалубные корабли");
                        Console.SetCursorPosition(50, 6);
                        Console.Write("Для этого выберите нужную клетку, написав букву и цифру, например: B4");
                        Console.SetCursorPosition(50, 7);
                    }
                    else if (curPlayer == 2)
                    {
                        Console.SetCursorPosition(1, 5);
                        Console.Write("Поставьте однопалубные корабли");
                        Console.SetCursorPosition(1, 6);
                        Console.Write("Для этого выберите нужную клетку,");
                        Console.SetCursorPosition(1, 7);
                        Console.Write("написав букву и цифру,");
                        Console.SetCursorPosition(1, 8);
                        Console.Write("например: B4");
                        Console.SetCursorPosition(1, 9);
                    }
                    s = Console.ReadLine();
                    if (s.Length == 2 || s.Length == 3)
                    {
                        iCurs.Add(Parsing(s)[0]);
                        jCurs.Add(Parsing(s)[1]);
                        if (iCurs[0] == -1 || jCurs[0] == -1)
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(1, curPlayer);
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }

                    // проверка на отсутствие кораблей рядом
                    if (field[iCurs[0], jCurs[0]] == 'S')
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(3, curPlayer);
                        continue;
                    }
                    if (iCurs[0] > 0)
                    {
                        if (field[iCurs[0] - 1, jCurs[0]] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            continue;
                        }
                        if (jCurs[0] > 0 && (field[iCurs[0] - 1, jCurs[0] - 1] == 'S' || field[iCurs[0], jCurs[0] - 1] == 'S'))
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            continue;
                        }
                        if (jCurs[0] < 9 && (field[iCurs[0] - 1, jCurs[0] + 1] == 'S' || field[iCurs[0], jCurs[0] + 1] == 'S'))
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            continue;
                        }
                    }
                    if (iCurs[0] < 9)
                    {
                        if (field[iCurs[0] + 1, jCurs[0]] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            continue;
                        }
                        if (jCurs[0] < 9 && field[iCurs[0] + 1, jCurs[0] + 1] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            continue;
                        }
                        if (jCurs[0] > 0 && field[iCurs[0] + 1, jCurs[0] - 1] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            continue;
                        }
                    }

                    canPlace = true;
                    field[iCurs[0], jCurs[0]] = 'S';
                    Console.Clear();
                    DrowField(field, curPlayer);
                }
            }

            // расстановка двухпалубных кораблей
            for (int i = 0; i < 3; i++)
            {
                canPlace = false;
                while (!canPlace)
                {
                    iCurs.Clear();
                    jCurs.Clear();
                    koord1.Clear();
                    koord2.Clear();
                    if (curPlayer == 1)
                    {
                        Console.SetCursorPosition(50, 5);
                        Console.Write("Поставьте двухпалубные корабли");
                        Console.SetCursorPosition(50, 6);
                        Console.Write("Для этого выберите несколько клеток,");
                        Console.SetCursorPosition(50, 7);
                        Console.Write("написав букву и цифру каждой, например: B4B5");
                        Console.SetCursorPosition(50, 8);
                    }
                    else if (curPlayer == 2)
                    {
                        Console.SetCursorPosition(1, 5);
                        Console.Write("Поставьте двухпалубные корабли");
                        Console.SetCursorPosition(1, 6);
                        Console.Write("Для этого выберите несколько клеток,");
                        Console.SetCursorPosition(1, 7);
                        Console.Write("написав букву и цифру каждой,");
                        Console.SetCursorPosition(1, 8);
                        Console.Write("например: B4B5");
                        Console.SetCursorPosition(1, 9);
                    }
                    s = Console.ReadLine();

                    // проверка на корректность ввода взависимости от количества символов
                    if (s.Length == 4)
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]);
                    }
                    else if (s.Length == 5)
                    {
                        if (s[2] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                            koord2.Append(s[3]).Append(s[4]);
                        }
                        else if (s[4] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]);
                            koord2.Append(s[2]).Append(s[3]).Append(s[4]);
                        }
                        else
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(1, curPlayer);
                            continue;
                        }
                    }
                    else if (s.Length == 6)
                    {
                        if (s[2] == '0' && s[5] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                            koord2.Append(s[3]).Append(s[4]).Append(s[5]);
                        }
                        else
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(1, curPlayer);
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }

                    iCurs.Add(Parsing(koord1.ToString())[0]);
                    jCurs.Add(Parsing(koord1.ToString())[1]);
                    iCurs.Add(Parsing(koord2.ToString())[0]);
                    jCurs.Add(Parsing(koord2.ToString())[1]);

                    if (iCurs[0] == -1 || jCurs[0] == -1 || iCurs[1] == -1 || jCurs[1] == -1)
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }

                    // проверка на то, что корабль цельный и не стоит по диагонали
                    if (iCurs[1] != iCurs[0] + 1 && iCurs[1] != iCurs[0] - 1 && jCurs[1] != jCurs[0] + 1 && jCurs[1] != jCurs[0] - 1)
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }
                    if ((iCurs[1] == iCurs[0] + 1 || iCurs[1] == iCurs[0] - 1) && jCurs[1] != jCurs[0])
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }
                    if ((jCurs[1] == jCurs[0] + 1 || jCurs[1] == jCurs[0] - 1) && iCurs[1] != iCurs[0])
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }

                    // проверка на отсутствие кораблей рядом
                    bool flag = false;
                    for (int j = 0; j < 2; j++)
                    {
                        if (field[iCurs[j], jCurs[j]] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }

                        if (iCurs[j] != 0)
                        {
                            if (field[iCurs[j] - 1, jCurs[j]] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] != 0 && (field[iCurs[j] - 1, jCurs[j] - 1] == 'S' || field[iCurs[j], jCurs[j] - 1] == 'S'))
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] != 9 && (field[iCurs[j] - 1, jCurs[j] + 1] == 'S' || field[iCurs[j], jCurs[j] + 1] == 'S'))
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                        }
                        if (iCurs[j] != 9)
                        {
                            if (field[iCurs[j] + 1, jCurs[j]] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] != 9 && field[iCurs[j] + 1, jCurs[j] + 1] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] > 0 && field[iCurs[j] + 1, jCurs[j] - 1] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                        }
                    }

                    if (flag)
                        continue;

                    for (int k = 0; k < 2; k++)
                    {
                        field[iCurs[k], jCurs[k]] = 'S';
                    }
                    canPlace = true;
                    Console.Clear();
                    DrowField(field, curPlayer);
                }
            }

            // расстановка трехпалубных кораблей
            for (int i = 0; i < 2; i++)
            {
                canPlace = false;
                while (!canPlace)
                {
                    iCurs.Clear();
                    jCurs.Clear();
                    koord1.Clear();
                    koord2.Clear();
                    koord3.Clear();
                    if (curPlayer == 1)
                    {
                        Console.SetCursorPosition(50, 5);
                        Console.Write("Поставьте трехпалубные корабли");
                        Console.SetCursorPosition(50, 6);
                        Console.Write("Для этого выберите несколько клеток,");
                        Console.SetCursorPosition(50, 7);
                        Console.Write("написав букву и цифру каждой, например: B4B5B6");
                        Console.SetCursorPosition(50, 8);
                    }
                    else if (curPlayer == 2)
                    {
                        Console.SetCursorPosition(1, 5);
                        Console.Write("Поставьте трехпалубные корабли");
                        Console.SetCursorPosition(1, 6);
                        Console.Write("Для этого выберите несколько клеток,");
                        Console.SetCursorPosition(1, 7);
                        Console.Write("написав букву и цифру каждой,");
                        Console.SetCursorPosition(1, 8);
                        Console.Write("например: B4B5B6");
                        Console.SetCursorPosition(1, 9);
                    }
                    s = Console.ReadLine();

                    // проверка на корректность ввода взависимости от количества символов
                    if (s.Length == 6)
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]);
                        koord3.Append(s[4]).Append(s[5]);
                    }
                    else if (s.Length == 7)
                    {
                        if (s[2] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                            koord2.Append(s[3]).Append(s[4]);
                            koord3.Append(s[5]).Append(s[6]);
                        }
                        else if (s[4] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]);
                            koord2.Append(s[2]).Append(s[3]).Append(s[4]);
                            koord3.Append(s[5]).Append(s[6]);
                        }
                        else if (s[6] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]);
                            koord2.Append(s[2]).Append(s[3]);
                            koord3.Append(s[4]).Append(s[5]).Append(s[6]);
                        }
                        else
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(1, curPlayer);
                            continue;
                        }

                    }
                    else if (s.Length == 8)
                    {
                        if (s[2] == '0' && s[5] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                            koord2.Append(s[3]).Append(s[4]).Append(s[5]);
                            koord3.Append(s[6]).Append(s[7]);
                        }
                        else if (s[2] == '0' && s[7] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                            koord2.Append(s[3]).Append(s[4]);
                            koord3.Append(s[5]).Append(s[6]).Append(s[7]);
                        }
                        else if (s[4] == '0' && s[7] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]);
                            koord2.Append(s[2]).Append(s[3]).Append(s[4]);
                            koord3.Append(s[5]).Append(s[6]).Append(s[7]);
                        }
                        else
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(1, curPlayer);
                            continue;
                        }
                    }
                    else if (s.Length == 9)
                    {
                        if (s[2] == '0' && s[5] == '0' && s[8] == '0')
                        {
                            koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                            koord2.Append(s[3]).Append(s[4]).Append(s[5]);
                            koord3.Append(s[6]).Append(s[7]).Append(s[8]);
                        }
                        else
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(1, curPlayer);
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }

                    iCurs.Add(Parsing(koord1.ToString())[0]);
                    jCurs.Add(Parsing(koord1.ToString())[1]);
                    iCurs.Add(Parsing(koord2.ToString())[0]);
                    jCurs.Add(Parsing(koord2.ToString())[1]);
                    iCurs.Add(Parsing(koord3.ToString())[0]);
                    jCurs.Add(Parsing(koord3.ToString())[1]);

                    if (iCurs[0] == -1 || jCurs[0] == -1 || iCurs[1] == -1 || jCurs[1] == -1 || iCurs[2] == -1 || jCurs[2] == -1)
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }

                    // проверка на то, что корабль цельный и не стоит по диагонали
                    if (iCurs[2] != iCurs[1] + 1 && iCurs[2] != iCurs[1] - 1 && jCurs[2] != jCurs[1] + 1 && jCurs[2] != jCurs[1] - 1)
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }

                    if (iCurs[2] == iCurs[1] + 1 && iCurs[1] != iCurs[0] + 1 || iCurs[2] == iCurs[1] - 1 && iCurs[1] != iCurs[0] - 1)
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }

                    if (jCurs[2] == jCurs[1] + 1 && jCurs[1] != jCurs[0] + 1 || jCurs[2] == jCurs[1] - 1 && jCurs[1] != jCurs[0] - 1)
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }

                    if ((iCurs[1] == iCurs[0] + 1 || iCurs[1] == iCurs[0] - 1) && jCurs[1] != jCurs[0])
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }
                    if ((iCurs[2] == iCurs[1] + 1 || iCurs[2] == iCurs[1] - 1) && jCurs[2] != jCurs[1])
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }
                    if ((jCurs[1] == jCurs[0] + 1 || jCurs[1] == jCurs[0] - 1) && iCurs[1] != iCurs[0])
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }
                    if ((jCurs[2] == jCurs[1] + 1 || jCurs[2] == jCurs[1] - 1) && iCurs[2] != iCurs[1])
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(2, curPlayer);
                        continue;
                    }

                    bool flag = false;

                    // проверка на отсутствие кораблей рядом
                    for (int j = 0; j < 3; j++)
                    {
                        if (field[iCurs[j], jCurs[j]] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }

                        if (iCurs[j] != 0)
                        {
                            if (field[iCurs[j] - 1, jCurs[j]] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] != 0 && (field[iCurs[j] - 1, jCurs[j] - 1] == 'S' || field[iCurs[j], jCurs[j] - 1] == 'S'))
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] != 9 && (field[iCurs[j] - 1, jCurs[j] + 1] == 'S' || field[iCurs[j], jCurs[j] + 1] == 'S'))
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                        }
                        if (iCurs[j] != 9)
                        {
                            if (field[iCurs[j] + 1, jCurs[j]] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] != 9 && field[iCurs[j] + 1, jCurs[j] + 1] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                            if (jCurs[j] > 0 && field[iCurs[j] + 1, jCurs[j] - 1] == 'S')
                            {
                                Console.Clear();
                                DrowField(field, curPlayer);
                                Exceptions(3, curPlayer);
                                flag = true;
                                break;
                            }
                        }
                    }

                    if (flag)
                        continue;

                    for (int k = 0; k < 3; k++)
                    {
                        field[iCurs[k], jCurs[k]] = 'S';
                    }
                    canPlace = true;
                    Console.Clear();
                    DrowField(field, curPlayer);
                }
            }

            // расстановка четырехпалубного корабля
            canPlace = false;
            while (!canPlace)
            {
                iCurs.Clear();
                jCurs.Clear();
                koord1.Clear();
                koord2.Clear();
                koord3.Clear();
                koord4.Clear();
                if (curPlayer == 1)
                {
                    Console.SetCursorPosition(50, 5);
                    Console.Write("Поставьте четырехпалубный корабль");
                    Console.SetCursorPosition(50, 6);
                    Console.Write("Для этого выберите несколько клеток,");
                    Console.SetCursorPosition(50, 7);
                    Console.Write("написав букву и цифру каждой, например: B4B5B6B7");
                    Console.SetCursorPosition(50, 8);
                }
                else if (curPlayer == 2)
                {
                    Console.SetCursorPosition(1, 5);
                    Console.Write("Поставьте четырехпалубный корабль");
                    Console.SetCursorPosition(1, 6);
                    Console.Write("Для этого выберите несколько клеток,");
                    Console.SetCursorPosition(1, 7);
                    Console.Write("написав букву и цифру каждой,");
                    Console.SetCursorPosition(1, 8);
                    Console.Write("например: B4B5B6B7");
                    Console.SetCursorPosition(1, 9);
                }
                s = Console.ReadLine();

                // проверка на корректность ввода взависимости от количества символов
                if (s.Length == 8)
                {
                    koord1.Append(s[0]).Append(s[1]);
                    koord2.Append(s[2]).Append(s[3]);
                    koord3.Append(s[4]).Append(s[5]);
                    koord4.Append(s[6]).Append(s[7]);
                }
                else if (s.Length == 9)
                {
                    if (s[2] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]);
                        koord4.Append(s[7]).Append(s[8]);
                    }
                    else if (s[4] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]);
                        koord4.Append(s[7]).Append(s[8]);
                    }
                    else if (s[6] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]);
                        koord3.Append(s[4]).Append(s[5]).Append(s[6]);
                        koord4.Append(s[7]).Append(s[8]);
                    }
                    else if (s[8] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]);
                        koord3.Append(s[4]).Append(s[5]);
                        koord4.Append(s[6]).Append(s[7]).Append(s[8]);
                    }
                    else
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }

                }
                else if (s.Length == 10)
                {
                    if (s[2] == '0' && s[5] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]).Append(s[5]);
                        koord3.Append(s[6]).Append(s[7]);
                        koord4.Append(s[8]).Append(s[9]);
                    }
                    else if (s[2] == '0' && s[7] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]).Append(s[7]);
                        koord4.Append(s[8]).Append(s[9]);
                    }
                    else if (s[2] == '0' && s[9] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]);
                        koord4.Append(s[7]).Append(s[8]).Append(s[9]);
                    }
                    else if (s[4] == '0' && s[7] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]).Append(s[7]);
                        koord4.Append(s[8]).Append(s[9]);
                    }
                    else if (s[4] == '0' && s[9] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]);
                        koord4.Append(s[7]).Append(s[8]).Append(s[9]);
                    }
                    else if (s[6] == '0' && s[9] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]);
                        koord3.Append(s[4]).Append(s[5]).Append(s[6]);
                        koord4.Append(s[7]).Append(s[8]).Append(s[9]);
                    }
                    else
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }
                }
                else if (s.Length == 11)
                {
                    if (s[2] == '0' && s[5] == '0' && s[8] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]).Append(s[5]);
                        koord3.Append(s[6]).Append(s[7]).Append(s[8]);
                        koord4.Append(s[9]).Append(s[10]);
                    }
                    else if (s[2] == '0' && s[5] == '0' && s[10] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]).Append(s[5]);
                        koord3.Append(s[6]).Append(s[7]);
                        koord4.Append(s[8]).Append(s[9]).Append(s[10]);
                    }
                    else if (s[2] == '0' && s[7] == '0' && s[10] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]).Append(s[7]);
                        koord4.Append(s[8]).Append(s[9]).Append(s[10]);
                    }
                    else if (s[4] == '0' && s[7] == '0' && s[10] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]);
                        koord2.Append(s[2]).Append(s[3]).Append(s[4]);
                        koord3.Append(s[5]).Append(s[6]).Append(s[7]);
                        koord4.Append(s[8]).Append(s[9]).Append(s[10]);
                    }
                    else
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }
                }
                else if (s.Length == 12)
                {
                    if (s[2] == '0' && s[5] == '0' && s[8] == '0' && s[11] == '0')
                    {
                        koord1.Append(s[0]).Append(s[1]).Append(s[2]);
                        koord2.Append(s[3]).Append(s[4]).Append(s[5]);
                        koord3.Append(s[6]).Append(s[7]).Append(s[8]);
                        koord4.Append(s[9]).Append(s[10]).Append(s[11]);
                    }
                    else
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(1, curPlayer);
                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(1, curPlayer);
                    continue;
                }

                iCurs.Add(Parsing(koord1.ToString())[0]);
                jCurs.Add(Parsing(koord1.ToString())[1]);
                iCurs.Add(Parsing(koord2.ToString())[0]);
                jCurs.Add(Parsing(koord2.ToString())[1]);
                iCurs.Add(Parsing(koord3.ToString())[0]);
                jCurs.Add(Parsing(koord3.ToString())[1]);
                iCurs.Add(Parsing(koord4.ToString())[0]);
                jCurs.Add(Parsing(koord4.ToString())[1]);

                if (iCurs[0] == -1 || jCurs[0] == -1 || iCurs[1] == -1 || jCurs[1] == -1 || iCurs[2] == -1 || jCurs[2] == -1 || iCurs[3] == -1 || jCurs[3] == -1)
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(1, curPlayer);
                    continue;
                }

                // проверка на то, что корабль цельный и не стоит по диагонали
                if (iCurs[3] != iCurs[2] + 1 && iCurs[3] != iCurs[2] - 1 && jCurs[3] != jCurs[2] + 1 && jCurs[3] != jCurs[2] - 1)
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }

                if (iCurs[3] == iCurs[2] + 1 && iCurs[2] != iCurs[1] + 1 || iCurs[3] == iCurs[2] - 1 && iCurs[2] != iCurs[1] - 1)
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }
                if (iCurs[2] == iCurs[1] + 1 && iCurs[1] != iCurs[0] + 1 || iCurs[2] == iCurs[1] - 1 && iCurs[1] != iCurs[0] - 1)
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }

                if (jCurs[3] == jCurs[2] + 1 && jCurs[2] != jCurs[1] + 1 || jCurs[3] == jCurs[2] - 1 && jCurs[2] != jCurs[1] - 1)
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }
                if (jCurs[2] == jCurs[1] + 1 && jCurs[1] != jCurs[0] + 1 || jCurs[2] == jCurs[1] - 1 && jCurs[1] != jCurs[0] - 1)
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }

                if ((iCurs[1] == iCurs[0] + 1 || iCurs[1] == iCurs[0] - 1) && jCurs[1] != jCurs[0])
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer); ;
                    continue;
                }
                if ((iCurs[2] == iCurs[1] + 1 || iCurs[2] == iCurs[1] - 1) && jCurs[2] != jCurs[1])
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }
                if ((iCurs[3] == iCurs[2] + 1 || iCurs[3] == iCurs[2] - 1) && jCurs[3] != jCurs[2])
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }
                if ((jCurs[1] == jCurs[0] + 1 || jCurs[1] == jCurs[0] - 1) && iCurs[1] != iCurs[0])
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }
                if ((jCurs[2] == jCurs[1] + 1 || jCurs[2] == jCurs[1] - 1) && iCurs[2] != iCurs[1])
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }
                if ((jCurs[3] == jCurs[2] + 1 || jCurs[3] == jCurs[2] - 1) && iCurs[3] != iCurs[2])
                {
                    Console.Clear();
                    DrowField(field, curPlayer);
                    Exceptions(2, curPlayer);
                    continue;
                }

                // проверка на отсутствие кораблей рядом
                bool flag = false;
                for (int j = 0; j < 4; j++)
                {
                    if (field[iCurs[j], jCurs[j]] == 'S')
                    {
                        Console.Clear();
                        DrowField(field, curPlayer);
                        Exceptions(3, curPlayer);
                        flag = true;
                        break;
                    }

                    if (iCurs[j] != 0)
                    {
                        if (field[iCurs[j] - 1, jCurs[j]] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }

                        if (jCurs[j] != 0 && (field[iCurs[j] - 1, jCurs[j] - 1] == 'S' || field[iCurs[j], jCurs[j] - 1] == 'S'))
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }
                        if (jCurs[j] != 9 && (field[iCurs[j] - 1, jCurs[j] + 1] == 'S' || field[iCurs[j], jCurs[j] + 1] == 'S'))
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }
                    }
                    if (iCurs[j] != 9)
                    {
                        if (field[iCurs[j] + 1, jCurs[j]] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }
                        if (jCurs[j] != 9 && field[iCurs[j] + 1, jCurs[j] + 1] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }
                        if (jCurs[j] > 0 && field[iCurs[j] + 1, jCurs[j] - 1] == 'S')
                        {
                            Console.Clear();
                            DrowField(field, curPlayer);
                            Exceptions(3, curPlayer);
                            flag = true;
                            break;
                        }
                    }
                }

                if (flag)
                    continue;

                for (int k = 0; k < 4; k++)
                {
                    field[iCurs[k], jCurs[k]] = 'S';
                }
                canPlace = true;
                Console.Clear();
                DrowField(field, curPlayer);
            }

            return field;
        }

        // функция обработки ошибочного ввода координат
        static void Exceptions(int numExp, int curPlayer)
        {
            string message;
            switch (numExp)
            {
                case 1:
                    message = "Это не набор координат";
                    break;
                case 2:
                    message = "Так нельзя поставить корабль";
                    break;
                case 3:
                    message = "Корабль находится рядом с другим";
                    break;
                default:
                    message = "Неверный ход";
                    break;
            }

            if (curPlayer == 1)
            {
                Console.SetCursorPosition(50, 10);
                Console.WriteLine(message);

            }
            else if (curPlayer == 2)
            {
                Console.SetCursorPosition(1, 10);
                Console.WriteLine(message);
            }
        }

        // функция совершения ходов
        static char[,] Step(char[,] field, char[,] fieldGame, int curPlayer)
        {
            int iStep, jStep;
            string steps;
            bool canStep = true;
            while (canStep)
            {
                Console.SetCursorPosition(5, 25);
                steps = Console.ReadLine();
                if (steps.Length == 2 || steps.Length == 3)
                {
                    iStep = Parsing(steps)[0];
                    jStep = Parsing(steps)[1];
                }
                else
                {
                    Console.SetCursorPosition(5, 25);
                    Console.WriteLine("                                     ");
                    Console.SetCursorPosition(5, 26);
                    Console.WriteLine("Неверный ход           ");
                    continue;
                }

                if (iStep == -1 || jStep == -1)
                {
                    Console.SetCursorPosition(5, 25);
                    Console.WriteLine("                                     ");
                    Console.SetCursorPosition(5, 26);
                    Console.WriteLine("Неверный ход           ");
                    continue;
                }
                if (fieldGame[iStep, jStep] != ' ')
                {
                    Console.SetCursorPosition(5, 25);
                    Console.WriteLine("                                     ");
                    Console.SetCursorPosition(5, 26);
                    Console.WriteLine("Вы сюда уже били                ");
                    continue;
                }

                if (field[iStep, jStep] == 'S')
                {
                    fieldGame[iStep, jStep] = 'X';
                    DrowField(fieldGame, curPlayer);
                    if (IsEnd(fieldGame))
                    {
                        break;
                    }
                    Console.SetCursorPosition(5, 25);
                    Console.WriteLine("                                     ");
                    Console.SetCursorPosition(5, 26);
                    Console.WriteLine("Можете ударить еще раз");
                }
                else if (field[iStep, jStep] == ' ')
                {
                    fieldGame[iStep, jStep] = '.';
                    DrowField(fieldGame, curPlayer);
                    Console.SetCursorPosition(5, 25);
                    Console.WriteLine("                                     ");
                    canStep = false;
                }
            }
            return fieldGame;
        }

        // функция проверки на окончание игры
        static bool IsEnd(char[,] fildGame)
        {
            int k = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (fildGame[i, j] == 'X')
                        k++;
                }
            }
            if (k == 20)
                return true;

            return false;
        }

        static void Main(string[] args)

        {
            char[,] field1 = new char[10, 10];
            char[,] field2 = new char[10, 10];
            char[,] fieldGame1 = new char[10, 10];
            char[,] fieldGame2 = new char[10, 10];
            int curPlayer = 1;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field1[i, j] = ' ';
                    field2[i, j] = ' ';
                    fieldGame1[i, j] = ' ';
                    fieldGame2[i, j] = ' ';
                }
            }
            field1 = Preparing(field1, 1);
            field2 = Preparing(field2, 2);
            DrowField(fieldGame1, 1);
            DrowField(fieldGame2, 2);

            while (true)
            {
                Console.SetCursorPosition(5, 23);
                Console.WriteLine($"Ходит {curPlayer} игрок");
                Console.SetCursorPosition(5, 24);
                Console.WriteLine("Введите координату клетки куда хотите ударить, например: A6");
                if (curPlayer == 1)
                {
                    fieldGame2 = Step(field2, fieldGame2, 2);
                }
                else if (curPlayer == 2)
                {
                    fieldGame1 = Step(field1, fieldGame1, 1);
                }
                if (curPlayer == 1)
                {
                    if (IsEnd(fieldGame2))
                    {
                        Console.SetCursorPosition(5, 25);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Выйграл 1 игрок");
                        break;
                    }
                    curPlayer = 2;
                }
                else
                {
                    if (IsEnd(fieldGame1))
                    {
                        Console.SetCursorPosition(5, 25);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Выйграл 2 игрок");
                        break;
                    }
                    curPlayer = 1;
                }
            }
        }
    }
}