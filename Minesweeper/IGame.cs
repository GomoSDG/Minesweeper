using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface IGame
    {
        void GameLost();
        void CheckWin();
        Square[,] getSquares();
        void generateMines();
        event delGameLost HitMine;
    }
}
