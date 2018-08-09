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

        public Board(int x, int y) {
            this.height = x;
            this.width = y;
            this.arrayCell = emptyArrayCell(x, y);
        }

        public static Cell[ ] emptyArrayCell(int countRow, int countColumn) {
            int countCells = countRow * countColumn;
            Cell[ ] cells = new Cell[countCells];
            for (int number = 0; number < countCells; number++) {
                int x = (int)Math.Floor((decimal)(number / countColumn));
                int y = number - x * countColumn;
                Cell cell = new Cell(x, y, false);
                cells[number] = cell;
            }
            return cells;
        }

        public static Cell[] arrayCellFromString(int countRow, int countColumn, String stringArrayCell) {
            int countCells = countRow * countColumn;
            Cell[ ] cells = new Cell[countCells];
            String[ ] stringForInt = stringArrayCell.Split(' ');
            for (int number = 0; number < countCells; number++) {
                Boolean living = ((int)Convert.ToInt32(stringForInt[number]) == 1) ? true : false;
                int y = (int)Math.Floor((decimal)(number / countColumn));
                int x = number - y * countColumn;
                Cell cell = new Cell(x, y, living);
                cells[number] = cell;
            }
            return cells;
        }

        public Boolean isEquals(Board board) {
            Boolean equels = true;
            if (this.height != board.height || this.width != board.width) return !equels;
            for (int number = 0; number < this.width * this.height; number++) {
                if (this.arrayCell[number].isLiving != board.arrayCell[number].isLiving) {
                    equels = !equels;
                    break;
                }
            }
            return equels;
        }

        public override String ToString() {
            String toPrint = "\n";
            for (int row = 0; row < this.width; row++) {
                for (int column = 0; column < this.height; column++) {
                    int result = (this.arrayCell[this.height * row + column].isLiving) ? 1 : 0;
                    toPrint += result.ToString()  + " ";
                }
                toPrint += "\n";
            }
            toPrint += "***************************\n";
            return toPrint;
        }
    }
}
