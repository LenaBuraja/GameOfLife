using System;
using System.IO;

namespace GameOfLife {
    class Program {

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

        static Cell[] OneStep(int n, int m, Cell[] inArrayCells) {
            Cell[ ] outArrayCells = new Cell[n * m];
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
                            livingCellsNumber += (inArrayCells[correctY * m + correctX].isLiving) ? 1 : 0;
                        }
                    }
                    Boolean isLiving;
                    if (inArrayCells[row * m + column].isLiving) {
                        livingCellsNumber--;
                        isLiving = (livingCellsNumber != 2 && livingCellsNumber != 3) ? false : true;
                    } else {
                        isLiving = (livingCellsNumber == 3) ? true : false;
                    }
                    Cell addCell = new Cell(column, row, isLiving);
                    outArrayCells[row * m + column] = addCell;
                }
            }
            return outArrayCells;
        }

        static void Main(string[ ] args) {
            //Console.Write("Enter path to file: ");
            //String path = Console.ReadLine( );
            String path = @"F:\\myFile.txt";
            int n = 0, m = 0;
            String stringArray = ReadFile(path, out n, out m);
            if (stringArray != "") {
                Board board = new Board(m, n, stringArray);
                Console.Write(board.ToString( ));
                while (true) {
                    Board newBoard = new Board(m, n);
                    newBoard.arrayCell = OneStep(n, m, board.arrayCell);
                    board.arrayCell = newBoard.arrayCell;
                    Console.Write(board.ToString( ));
                    if (board.isEquals(new Board(m, n))) break;
                    Console.ReadLine( );
                }
                
            }
                Console.ReadLine( );
        }
    }
}
