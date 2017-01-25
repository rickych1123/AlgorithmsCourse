using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(returnIsTheStringNumber().ToString());
            percolationTest();
        }

        private void percolationTest()
        {

            int N = Convert.ToInt32(comboBox1.Text);
            //tableLayoutPanel1.ColumnCount = N;
            //tableLayoutPanel1.RowCount = N;
            Stopwatch timer = new Stopwatch();
            Random r = new Random();
            bool[,] open = new bool[N, N];
            int numOfOpenBlock = 0;
            int a = 0;
            int b = 0;
            timer.Start();
            while (!percolates(open))
            {
                while (true)
                {
                    a = r.Next(0, N);
                    b = r.Next(0, N);
                    if (!open[a, b])
                    {
                        open[a, b] = true;
                        break;
                    }

                }
                numOfOpenBlock++;
            }
            int[] keyPoint = new int[] { a, b };
            timer.Stop();
            bool[,] waterLine = getWaterLine(open);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (open[i, j])
                    {
                        textBox1.Text += "1";

                    }
                    else
                        textBox1.Text += "0";
                }
                textBox1.Text += "\r\n";
            }
            textBox1.Text += string.Format("Matrix: {0} x {1} ", open.GetLength(0), open.GetLength(1));
            textBox1.Text += "\r\n";
            textBox1.Text += "Open Block Number: " + numOfOpenBlock.ToString();
            textBox1.Text += "\r\n";
            textBox1.Text += "Time eclipse: " + timer.ElapsedMilliseconds.ToString();
            textBox1.Text += "\r\n";
            drawForm draw = new drawForm(open, N, waterLine,keyPoint);
            draw.Show();
        }

        private bool[,] getWaterLine(bool[,] open)
        {
            bool[,] waterLine;
            while (true)
            {
                for (int j = 0; j < open.GetLength(0); j++)
                {
                    waterLine = new bool[open.GetLength(0), open.GetLength(1)];
                    flow(open, waterLine, 0, j);
                    for (int k = 0; k < open.GetLength(0); k++)
                    {
                        if (waterLine[open.GetLength(0) - 1, k]) return waterLine;
                    }
                }
            }
        }
        private static void flow(bool[,] open, bool[,] full, int i, int j)
        {
            int N = open.GetLength(0);

            // base cases
            if (i < 0 || i >= N) return;    // invalid row
            if (j < 0 || j >= N) return;    // invalid column
            if (!open[i, j]) return;        // not an open site
            if (full[i, j]) return;         // already marked as full

            // mark i-j as full
            full[i, j] = true;

            flow(open, full, i + 1, j);   // down
            flow(open, full, i, j + 1);   // right
            flow(open, full, i, j - 1);   // left
            flow(open, full, i - 1, j);   // up
        }
        public static bool percolates(bool[,] open)
        {
            int N = open.GetLength(0);
            bool[,] full = flow(open);
            for (int j = 0; j < N; j++)
            {
                if (full[N - 1, j]) return true;
            }
            return false;
        }
        public static bool[,] flow(bool[,] open)
        {
            int N = open.GetLength(0);
            bool[,] full = new bool[N, N];
            for (int j = 0; j < N; j++)
            {
                flow(open, full, 0, j);
            }
            return full;
        }





    }
}
