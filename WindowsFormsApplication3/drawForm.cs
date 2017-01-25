using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class drawForm : Form
    {
        int _N;
        bool[,] _open;
        bool[,] _waterLine;
        int[] _keyPoint;
        public drawForm(bool[,] open, int N, bool[,] waterLine, int[] keyPoint)
        {
            InitializeComponent();
            _open = open;
            _N = N;
            _waterLine = waterLine;
            this.Size = new Size(_N * 15, _N * 15);
            _keyPoint = keyPoint;
        }

        private void DrawChessBoard(Graphics graphics, int width)
        {
            //Draw bolder
            graphics.DrawRectangle(new Pen(Brushes.DimGray, 4), 15, 15, _N * width, _N * width);

            //Draw elements
            for (var i = 0; i < _N; i++)
            {
                for (var j = 0; j < _N; j++)
                {
                    if (_waterLine[i, j])
                        graphics.FillRectangle(Brushes.LightSkyBlue, 15 + j * width, 15 + i * width, width, width);
                    else
                        graphics.FillRectangle(_open[i, j] ? Brushes.White : Brushes.Black, 15 + j * width, 15 + i * width, width, width);
                    if (i == _keyPoint[0] && j == _keyPoint[1])
                        graphics.FillRectangle(Brushes.Orange, 15 + j * width, 15 + i * width, width, width);

                }
            }
        }

        private void drawForm_Paint(object sender, PaintEventArgs e)
        {

            DrawChessBoard(e.Graphics, 10);

        }
    }
}
