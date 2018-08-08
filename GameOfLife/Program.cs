using System;
using System.IO;

namespace GameOfLife {
    class Program {

        static void Print(int n, int m, int[ ][ ] array) {
            Console.WriteLine( );
            for (int row = 0; row < n; row++) {
                for (int column = 0; column < m; column++) {
                    Console.Write($"{array[row][column]}  ");
                }
                Console.WriteLine( );
            }
            Console.Write("***************************");
            Console.WriteLine( );
        }

        static String ReadFile(String path, out int n, out int m) {
            n = -1;
            m = -1;
            String stringFromFile = "";
            try {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default)) {
                    string[ ] mass = sr.ReadLine( ).Split(' ');
                    if (mass.Length != 2) throw new Exception( );
                    n = int.Parse(mass[0]);
                    m = int.Parse(mass[1]);
                    if (n < 0 || m < 0) throw new Exception( );
                    string line;
                    int countLine = n;
                    while ((line = sr.ReadLine( )) != null || countLine != 0) {
                        if (line.Split(' ').Length == m) {
                            stringFromFile += line + " ";
                            countLine--;
                        } else {
                            throw new Exception( );
                        }
                    }
                    if (countLine > 0 || countLine < 0) {
                        throw new Exception( );
                    }
                }
            } catch (Exception e) {
                stringFromFile = "";
                Console.WriteLine("File isn't correct.");
            }
            return stringFromFile;
        }

        static int[ ][ ] SetArray(int n, int m) {
            int[ ][ ] array = new int[n][ ];
            for (int row = 0; row < n; row++) {
                int[ ] arrayRow = new int[m];
                for (int i = 0; i < m; i++) {
                    arrayRow[i] = 0;
                }
                array[row] = arrayRow;
            }
            return array;
        }

        static int[ ][ ] SetArrayFromString(int n, int m, String stringArray) {
            int[ ][ ] array = new int[n][ ];
            for (int row = 0; row < n; row++) {
                String subStringArray = stringArray.Substring(row * m * 2, m * 2);
                String[ ] sstringForInt = subStringArray.Split(' ');
                int[ ] arrayRow = new int[m];
                for (int i = 0; i < sstringForInt.Length - 1; i++) {
                    arrayRow[i] = (int)Convert.ToInt32(sstringForInt[i]);
                }
                array[row] = arrayRow;
            }
            return array;
        }

        static int[][] OneStep(int n, int m, int[][] inArray) {
            int[ ][ ] outArray = new int[n][ ];
            outArray = SetArray(n, m);

            for (int row = 0; row < n; row++) {
                for (int column = 0; column < m; column++) {
                    int livingCellsNumber = 0;
                    for (int y = row - 1; y <= row + 1; y++) {
                        for (int x = column - 1; x <= column + 1; x++) {
                            int correctY = y;
                            int correctX = x;
                            if (correctY >= n) correctY = 0;
                            if (correctY < 0) correctY = n - 1;
                            if (correctX >= n) correctX = 0;
                            if (correctX < 0) correctX = n - 1;
                            livingCellsNumber += (inArray[correctY][correctX] == 1) ? 1 : 0;
                        }
                    }
                    if (inArray[row][column] == 1) {
                        livingCellsNumber--;
                        outArray[row][column] = (livingCellsNumber != 2 && livingCellsNumber != 3) ? 0 : 1;
                    } else {
                        outArray[row][column] = (livingCellsNumber == 3) ? 1 : 0;
                    }
                }
            }
            return outArray;
        }

        static void Main(string[ ] args) {
            Console.Write("Enter path to file: ");
            String path = Console.ReadLine( );
            int n = 0, m = 0;
            String stringArray = ReadFile(path, out n, out m);
            if (stringArray != "") {
                int[ ][ ] startArray = new int[n][ ];
                startArray = SetArrayFromString(n, m, stringArray);
                Print(n, m, startArray);
               while(true) {
                    int[ ][ ] nextArray = OneStep(n, m, startArray);
                    startArray = nextArray;
                    Print(n, m, startArray);
                    Console.ReadLine( );
                }
            }
                Console.ReadLine( );
        }
    }
}
