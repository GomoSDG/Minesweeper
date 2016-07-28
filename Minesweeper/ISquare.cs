using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface ISquare
    {
        bool hasMine { get; set; }
        bool isFlagged { get; set; }

        int NumberOfSurroundingMines { get; set; }
        void PopSquare();
        Point squareLocation { get; set; }
        bool isPopped { get; set; }
    }
}
