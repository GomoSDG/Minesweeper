using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class frmGame : Form, ISquareContainer
    {
        private IGame myGame;
        public frmGame()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            panel1.Controls.Clear();
            myGame = new Minesweeper.Game(10, 10, this);
            myGame.HitMine += MyGame_HitMine;
        }

        private void MyGame_HitMine()
        {
            MessageBox.Show("You Lose!");
            NewGame();
        }

        delegate void DrawDelegate(IGame drawGame);
        public void Draw(IGame drawGame)
        {
            if (panel1.InvokeRequired)
            {
                DrawDelegate del = new DrawDelegate(Draw);
                panel1.Invoke(del, drawGame);
            }
            else
            {
                for (int i = 0; i < drawGame.getSquares().GetLength(0); i++)
                {
                    for (int x = 0; x < drawGame.getSquares().GetLength(1); x++)
                    {
                        Button btn = drawGame.getSquares()[x, i];
                        btn.Left = btn.Width * x;
                        btn.Top = btn.Height * i;
                        panel1.Controls.Add(btn);
                    }
                }
                int lastControlIndex = panel1.Controls.Count - 1;

                panel1.ClientSize = new Size(panel1.Controls[lastControlIndex].Right, panel1.Controls[lastControlIndex].Bottom);
                panel1.Left = 20;
                panel1.Top = panel2.Bottom + 20;
                this.ClientSize = new Size(panel1.ClientSize.Width + 40, panel1.Bottom + 20);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}
