using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Square : Button, ISquare
    {
        private int iSurroundingMines;
        private IGame Game;
        private Thread squareThread;

        //Properties
        public bool isPopped { get; set; }
        public bool hasMine { get; set; }
        public bool isFlagged { get; set; }
        public Point squareLocation { get; set; }

        public int NumberOfSurroundingMines
        {
            get
            {
                return iSurroundingMines;
            }

            set
            {
                iSurroundingMines = value;
            }
        }

        public Square(IGame Game, Point Location, int Size = 22)
        {
            this.Size = new System.Drawing.Size(Size, Size);
            hasMine = false;
            iSurroundingMines = 0;
            this.MouseUp += Square_MouseClick;
            this.Game = Game;
            isPopped = false;
            squareLocation = Location;
        }

        void Square_MouseClick(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = (MouseEventArgs)e;
            if (mouseEvent.Button == MouseButtons.Left && !isPopped && !isFlagged)
            {
                PopSquare();
            }
            
            if(mouseEvent.Button == MouseButtons.Right && !isPopped)
            {
                if (isFlagged)
                {
                    this.BackColor = SystemColors.ControlLight;
                    this.isFlagged = false;
                }
                else
                {
                    this.BackColor = Color.OrangeRed;
                    this.isFlagged = true;
                }
            }
        }

        public void PopSquare()
        {
            isPopped = true;
            this.FlatStyle = FlatStyle.Flat;

            //Change image if bomb or number.


            //Find out if player won or lost
            if (hasMine)
            {
                this.Text = "X";
                this.BackColor = System.Drawing.Color.Red;
                Game.GameLost();
            }
            else
            {
                this.Text = (NumberOfSurroundingMines > 0) ? NumberOfSurroundingMines.ToString() : "";
                this.ForeColor = System.Drawing.Color.LawnGreen;

                //Pop Surrounding Squares;
                if (NumberOfSurroundingMines == 0)
                {
                    PopSurroundingEmptyTiles(squareLocation.X + 1, squareLocation.Y);
                    PopSurroundingEmptyTiles(squareLocation.X - 1, squareLocation.Y);
                    PopSurroundingEmptyTiles(squareLocation.X, squareLocation.Y + 1);
                    PopSurroundingEmptyTiles(squareLocation.X, squareLocation.Y - 1);

                }

                Game.CheckWin();
            }
        }

        private void PopSurroundingEmptyTiles(int x, int y)
        {
                try
                {
                    ISquare sideSquare = Game.getSquares()[x, y];
                    if (!sideSquare.hasMine && !sideSquare.isPopped)
                        sideSquare.PopSquare();
                }
                catch { }
        }
    }
}
