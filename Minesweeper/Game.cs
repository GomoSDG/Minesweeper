using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Game : IGame
    {
        private Square[,] gameSquares;
        private ISquareContainer gameSquareContainer;
        private Thread gameThread;

        public event delGameLost HitMine;

        public Game(int Width, int Height, ISquareContainer SquareContainer)
        {
            gameSquares = new Square[Width, Height];
            gameSquareContainer = SquareContainer;
            gameThread = new Thread(InitializeSquares);
            gameThread.Start();
        }

        private void InitializeSquares()
        {
            for (int y = 0; y < gameSquares.GetLength(0); y++)
            {
                for (int x = 0; x < gameSquares.GetLength(1); x++)
                {
                    gameSquares[x, y] = new Square(this, new System.Drawing.Point(x, y));
                }
            }
            generateMines();
            placeNumbers();
            gameSquareContainer.Draw(this);
            gameThread.Abort();
        }

        private void placeNumbers()
        {
            
            for (int y = 0; y < gameSquares.GetLength(0); y++)
            {
                for (int x = 0; x < gameSquares.GetLength(1); x++)
                {
                    int mineCount = 0;
                    ISquare square = gameSquares[x, y];
                    if (!square.hasMine)
                    {
                        mineCount = NumMines(mineCount, x + 1, y);
                        mineCount = NumMines(mineCount, x + 1, y - 1);
                        mineCount = NumMines(mineCount, x + 1, y + 1);
                        mineCount = NumMines(mineCount, x - 1, y);
                        mineCount = NumMines(mineCount, x - 1, y - 1);
                        mineCount = NumMines(mineCount, x - 1, y + 1);
                        mineCount = NumMines(mineCount, x, y - 1);
                        mineCount = NumMines(mineCount, x, y + 1);

                        square.NumberOfSurroundingMines = mineCount;
                    }
                }
            }
        }

        private int NumMines(int mineCount, int x, int y)
        {
            try
            {
                ISquare sideSquare = gameSquares[x, y];
                if (sideSquare.hasMine)
                    mineCount += 1;
            }
            catch { }

            return mineCount;
        }

        public void GameLost()
        {
            HitMine?.Invoke();
        }

        public void CheckWin()
        {
            
        }

        public Square[,] getSquares()
        {
            return gameSquares;
        }

        public void generateMines()
        {
            int bombCounter = 0;
            while (bombCounter < 15)
            {
                int x = GlobalVariables.rndm.Next(gameSquares.GetLength(0));
                int y = GlobalVariables.rndm.Next(gameSquares.GetLength(1));

                ISquare square = gameSquares[x, y];

                if (!square.hasMine)
                {
                    square.hasMine = true;
                    bombCounter++;
                }
            }
        }
    }
}
