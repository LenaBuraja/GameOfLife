using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife {
    class Board {
        public int height;
        public int width;
        public Cell[ ] arrayCell;

        public Board(int x, int y, String arrayCells) {
            this.height = x;
            this.width = y;
            this.arrayCell = arrayCellFromString(x, y, arrayCells);
        }

        public static Cell[] arrayCellFromString(int countRow, int countColumn, String stringArrayCell) {
            int countCells = countRow * countColumn;
            Cell[ ] cells = new Cell[countCells];
            for (int number = 0; number < countCells; number++) {
                Boolean living = ((int)Convert.ToInt32(stringArrayCell[number]) == 1) ? true : false;
                int x = (int)Math.Floor((decimal)(number / countColumn));
                int y = number - x * countColumn;
                Cell cell = new Cell(x, y, living);
                cells[number] = cell;
            }
            return cells;
        }

        public void ToString(int n, int m, int[ ][ ] array) {
            Console.WriteLine( );
            for (int row = 0; row < n; row++) {
                for (int column = 0; column < m; column++) {
                    int result = (this.arrayCell[column * row].isLiving) ? 1 : 0;
                    Console.Write($"{result}  ");
                }
                Console.WriteLine( );
            }
            Console.Write("***************************");
            Console.WriteLine( );
        }
    }
}
