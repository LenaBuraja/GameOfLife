using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife {
    class Cell {
        public int posotionX;
        public int positionY;
        public Boolean isLiving;

        public Cell(int x, int y) {
            this.posotionX = x;
            this.positionY = y;
            this.isLiving = false;
        }

        public Cell(int x, int y, Boolean living) {
            this.posotionX = x;
            this.positionY = y;
            this.isLiving = living;
        }
    }
}
