using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Gra_w_życie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            Animacja.Enabled = false;
        }

        Button[,] przycik;
        Button[,] pom;
        int x, y;
        Random rand = new Random();
        int pusty;

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    przycik[i, j].Enabled = false;
                    przycik[i, j].Visible = false;
                    Array.Clear(przycik, i, j);
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                x = 10;
                y = 10;
            }   
            if(comboBox1.SelectedIndex == 1)
            {
                x = 15;
                y = 10;
            }
            if(comboBox1.SelectedIndex == 2)
            {
                x = 15;
                y = 15;
            }
            przycik = new Button[x,y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    przycik[i, j] = new Button();
                    przycik[i, j].Width = panel1.Width/x;
                    przycik[i, j].Height = panel1.Height/x;
                    przycik[i, j].BackColor = Button.DefaultBackColor;
                    przycik[i, j].Left = (i * panel1.Width / x) + panel1.Left ;
                    przycik[i, j].Top = (j * panel1.Height / x) + panel1.Top ;
                    przycik[i, j].Click += new System.EventHandler(this.klik);
                    przycik[i, j].Enabled = true;
                    this.Controls.Add(przycik[i,j]);
                }
            }
            Animacja.Enabled = true;
        }

        private void Animacja_Click(object sender, EventArgs e)
        {
            pom = new Button[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    pom[i, j] = new Button();
                    if (sprawdz_sasiadow(i, j) == 3 && przycik[i, j].BackColor == Button.DefaultBackColor)
                    {
                        pom[i, j].BackColor = Color.Blue;
                    }
                    else if (przycik[i, j].BackColor == Color.Blue && sprawdz_sasiadow(i, j) == 2 || sprawdz_sasiadow(i, j) == 3)
                    {
                        pom[i, j].BackColor = Color.Blue;
                    }
                    else if (przycik[i, j].BackColor == Color.Blue)
                    {
                        pom[i, j].BackColor = Button.DefaultBackColor;
                    }
                    this.Controls.Add(pom[i, j]);
                }
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    przycik[i, j].BackColor = pom[i, j].BackColor;
                    Array.Clear(pom, i, j);
                }
            }

        }

        private int sprawdz_sasiadow(int x, int y)
        {
            int licznik = 0;
           
                try
                {
                    if (przycik[x - 1, y + 1].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
                try
                {
                    if (przycik[x, y + 1].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
                try
                {
                    if (przycik[x + 1, y + 1].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
                try
                {
                    if (przycik[x - 1, y].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
                try
                {
                    if (przycik[x + 1, y].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
                try
                {
                    if (przycik[x - 1, y - 1].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
                try
                {
                    if (przycik[x, y - 1].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
                try
                {
                    if (przycik[x + 1, y - 1].BackColor == Color.Blue)
                    {
                        licznik++;
                    }
                }
                catch
                { }
            return licznik;
        }

        private void animacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    przycik[i, j].BackColor = Color.White;
                }
            }
            Thread.Sleep(100);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    pusty = rand.Next(0, 2);
                    if (pusty == 1)
                        przycik[i, j].BackColor = Color.Blue;
                }
            }
        }

        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line = "";
            File.Delete(@"plansza.txt");
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if(przycik[i, j].BackColor == Button.DefaultBackColor)
                    {
                        line += 0;
                    }
                    else
                    {
                        line += 1;
                    }
                }
            }
            File.WriteAllText(@"plansza.txt", line);
        }

        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"plansza.txt"))
            {
                string line = "";
                line = File.ReadAllText(@"plansza.txt");
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        przycik[i, j].Enabled = false;
                        przycik[i, j].Visible = false;
                        Array.Clear(przycik, i, j);
                    }
                }
                if (line.Length == 100)
                {
                    x = 10;
                    y = 10;
                }
                if (line.Length == 150)
                {
                    x = 15;
                    y = 10;
                }
                if (line.Length == 225)
                {
                    x = 15;
                    y = 15;
                }
                int licznik = 0;
                przycik = new Button[x, y];
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        przycik[i, j] = new Button();
                        przycik[i, j].Width = panel1.Width / x;
                        przycik[i, j].Height = panel1.Height / x;
                        if (line[licznik].ToString() == "1")
                        {
                            przycik[i, j].BackColor = Color.Blue;
                        }
                        else
                        {
                            przycik[i, j].BackColor = Button.DefaultBackColor;
                        }
                        przycik[i, j].Left = (i * panel1.Width / x) + panel1.Left;
                        przycik[i, j].Top = (j * panel1.Height / x) + panel1.Top;
                        przycik[i, j].Click += new System.EventHandler(this.klik);
                        przycik[i, j].Enabled = true;
                        licznik++;
                        this.Controls.Add(przycik[i, j]);
                    }
                }
                Animacja.Enabled = true;

            }
            else
            {
                MessageBox.Show("Blad pliku");
            }
        }

        private void klik(object sender, EventArgs e)
        {
            Button przycisk = (sender as Button);
            przycisk.BackColor = Color.Blue;
        }
    }
}
