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

namespace CavalierID
{
    public partial class CavalierID : Form
    {
        private Button[,] grille;
        static int nombre = 1;
        static int score = 1, posibilité = 0, latestScrore = 0, last = 0;
        static Boolean go = false;
        static Boolean solution = true;
        List<int> retour = new List<int>();
        static int[,] echec = new int[12, 12];

        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        public CavalierID()
        {
            InitializeComponent();
        }

        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }
        private void CavalierID_Load(object sender, EventArgs e)
        {
            grille = new Button[8, 8];
            this.grille[0, 0] = button1;
            this.grille[0, 1] = button14;
            this.grille[0, 2] = button3;
            this.grille[0, 3] = button5;
            this.grille[0, 4] = button7;
            this.grille[0, 5] = button19;
            this.grille[0, 6] = button12;
            this.grille[0, 7] = button33;

            this.grille[1, 0] = button9;
            this.grille[1, 1] = button2;
            this.grille[1, 2] = button18;
            this.grille[1, 3] = button4;
            this.grille[1, 4] = button6;
            this.grille[1, 5] = button16;
            this.grille[1, 6] = button11;
            this.grille[1, 7] = button32;

            this.grille[2, 0] = button8;
            this.grille[2, 1] = button13;
            this.grille[2, 2] = button21;
            this.grille[2, 3] = button20;
            this.grille[2, 4] = button17;
            this.grille[2, 5] = button15;
            this.grille[2, 6] = button10;
            this.grille[2, 7] = button31;

            this.grille[3, 0] = button30;
            this.grille[3, 1] = button29;
            this.grille[3, 2] = button28;
            this.grille[3, 3] = button35;
            this.grille[3, 4] = button26;
            this.grille[3, 5] = button27;
            this.grille[3, 6] = button60;
            this.grille[3, 7] = button57;

            this.grille[4, 0] = button25;
            this.grille[4, 1] = button22;
            this.grille[4, 2] = button49;
            this.grille[4, 3] = button48;
            this.grille[4, 4] = button47;
            this.grille[4, 5] = button51;
            this.grille[4, 6] = button63;
            this.grille[4, 7] = button64;

            this.grille[5, 0] = button24;
            this.grille[5, 1] = button50;
            this.grille[5, 2] = button46;
            this.grille[5, 3] = button45;
            this.grille[5, 4] = button44;
            this.grille[5, 5] = button65;
            this.grille[5, 6] = button53;
            this.grille[5, 7] = button61;

            this.grille[6, 0] = button23;
            this.grille[6, 1] = button43;
            this.grille[6, 2] = button42;
            this.grille[6, 3] = button41;
            this.grille[6, 4] = button40;
            this.grille[6, 5] = button58;
            this.grille[6, 6] = button56;
            this.grille[6, 7] = button62;

            this.grille[7, 0] = button34;
            this.grille[7, 1] = button39;
            this.grille[7, 2] = button38;
            this.grille[7, 3] = button37;
            this.grille[7, 4] = button36;
            this.grille[7, 5] = button59;
            this.grille[7, 6] = button54;
            this.grille[7, 7] = button52;

            for (int l = 0; l < 8; l++)
            {
                for (int c = 0; c < 8; c++)
                {
                    grille[l, c].Enabled = false;
                }
            }

            button67.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button b = sender as Button;
                if (solution)
                {
                    nombre = 1;
                    go = false;
                    for (int l = 0; l < 8; l++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (b.Name == grille[l, c].Name)
                            {
                                excuterCavalier(l, c);
                            }
                        }
                    }
                    for (int l = 0; l < 8; l++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            grille[l, c].Text = "";
                        }
                    }
                    go = true;
                }
                if (!solution)
                {
                    button67.Enabled = true;
                    for (int l = 0; l < 8; l++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (l % 2 == 0)
                            {
                                if (c % 2 == 0) grille[l, c].BackColor = System.Drawing.Color.Beige;
                                else grille[l, c].BackColor = System.Drawing.Color.Chocolate;
                                
                            }
                            else
                            {
                                if (c % 2 == 0) grille[l, c].BackColor = System.Drawing.Color.Chocolate;
                                else grille[l, c].BackColor = System.Drawing.Color.Beige;
                            }

                        }
                    }
                    
                    for (int l = 0; l < 8; l++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (b.Name == grille[l, c].Name)
                            {
                                posibilité = 0;
                                grille[l, c].Text = "" + score;
                                richTextBox1.Text = richTextBox2.Text + " : " + score;
                                retour.Add(score);
                                
                                if (l + 2 < 8 && c - 1 >= 0) {
                                    if (grille[l + 2, c - 1].Text == "") { grille[l + 2, c - 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l + 2 < 8 && c + 1 < 8) {
                                    if (grille[l + 2, c + 1].Text == "") { grille[l + 2, c + 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l + 1 < 8 && c - 2 >= 0) {
                                    if (grille[l + 1, c - 2].Text == "") { grille[l + 1, c - 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l + 1 < 8 && c + 2 < 8) {
                                    if (grille[l + 1, c + 2].Text == "") { grille[l + 1, c + 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 2 >= 0 && c - 1 >= 0) {
                                    if (grille[l - 2, c - 1].Text == "") { grille[l - 2, c - 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 2 >= 0 && c + 1 < 8) {
                                    if (grille[l - 2, c + 1].Text == "") { grille[l - 2, c + 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 1 >= 0 && c - 2 >= 0) {
                                    if (grille[l - 1, c - 2].Text == "") { grille[l - 1, c - 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 1 >= 0 && c + 2 < 8) {
                                    if (grille[l - 1, c + 2].Text == "") { grille[l - 1, c + 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (posibilité == 0) {
                                    MessageBox.Show("Score : " + score + ". Pal mal, richTextBox2.Text, clique à nouveau sur Rejouer pour recommencer.",
                            "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if(latestScrore > score) richTextBox3.Text = richTextBox3.Text + richTextBox2.Text + " : " + score + "\n";
                                    else richTextBox3.Text = richTextBox2.Text + " : " + score + "\n" + richTextBox3.Text;
                                    button55.Text = "ReJouer";
                                    richTextBox2.Text = "";
                                    richTextBox2.Enabled = true;
                                    latestScrore = score;
                                }
                            }
                        }
                    }

                    for (int l = 0; l < 8; l++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (grille[l, c].BackColor != System.Drawing.Color.LimeGreen) grille[l, c].Enabled = false;
                            else grille[l, c].Enabled = true;
                        }
                    }
                    score++;
                }
            }
        }

        public void excuterCavalier(int l, int c)
        {
            int nb_fuite, min_fuite, lmin_fuite = 0;
            int i, j, k, m, ii, jj;

            ii = l + 1;
            jj = c + 1;

            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);


            i = ii + 1; j = jj + 1;
            echec[i, j] = 1;

            for (k = 2; k <= 64; k++)
            {
                for (m = 0, min_fuite = 11; m < 8; m++)
                {
                    ii = i + depi[m]; jj = j + depj[m];

                    nb_fuite = ((echec[ii, jj] != 0) ? 10 : fuite(ii, jj));

                    if (nb_fuite < min_fuite)
                    {
                        min_fuite = nb_fuite; lmin_fuite = m;
                    }
                }
                if (min_fuite == 9 & k != 64)
                {
                    Console.WriteLine("***   IMPASSE   ***");
                    break;
                }
                i += depi[lmin_fuite]; j += depj[lmin_fuite];
                echec[i, j] = k;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value;
            if (go == true)
            {
                for (int l = 0; l < 8; l++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        if (nombre == echec[l + 2, c + 2])
                        {
                            grille[l, c].Text = "" + nombre;
                        }
                    }
                }
                nombre++;
            }
            if (nombre > 64) button66.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (retour.Count == 0) button67.Enabled = false;
            else button67.Enabled = true;
        }


        private void button67_Click(object sender, EventArgs e)
        {
            score--;
            if (retour.Count != 0)
            {
                last = retour.Last();
                for (int l = 0; l < 8; l++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        if (grille[l, c].Text == "" + last)
                        {
                            grille[l, c].Text = "";
                            retour.Remove(last);
                        }
                        grille[l, c].Enabled = true;
                        if (l % 2 == 0)
                        {
                            if (c % 2 == 0) grille[l, c].BackColor = System.Drawing.Color.Beige;
                            else grille[l, c].BackColor = System.Drawing.Color.Chocolate;

                        }
                        else
                        {
                            if (c % 2 == 0) grille[l, c].BackColor = System.Drawing.Color.Chocolate;
                            else grille[l, c].BackColor = System.Drawing.Color.Beige;
                        }

                    }
                }
                if(retour.Count != 0)
                {
                    last = retour.Last();
                    for (int l = 0; l < 8; l++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (grille[l, c].Text == "" + last)
                            {
                                if (l + 2 < 8 && c - 1 >= 0)
                                {
                                    if (grille[l + 2, c - 1].Text == "") { grille[l + 2, c - 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l + 2 < 8 && c + 1 < 8)
                                {
                                    if (grille[l + 2, c + 1].Text == "") { grille[l + 2, c + 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l + 1 < 8 && c - 2 >= 0)
                                {
                                    if (grille[l + 1, c - 2].Text == "") { grille[l + 1, c - 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l + 1 < 8 && c + 2 < 8)
                                {
                                    if (grille[l + 1, c + 2].Text == "") { grille[l + 1, c + 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 2 >= 0 && c - 1 >= 0)
                                {
                                    if (grille[l - 2, c - 1].Text == "") { grille[l - 2, c - 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 2 >= 0 && c + 1 < 8)
                                {
                                    if (grille[l - 2, c + 1].Text == "") { grille[l - 2, c + 1].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 1 >= 0 && c - 2 >= 0)
                                {
                                    if (grille[l - 1, c - 2].Text == "") { grille[l - 1, c - 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                                if (l - 1 >= 0 && c + 2 < 8)
                                {
                                    if (grille[l - 1, c + 2].Text == "") { grille[l - 1, c + 2].BackColor = System.Drawing.Color.LimeGreen; posibilité++; }
                                }
                            }
                        }
                    }

                    for (int l = 0; l < 8; l++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (grille[l, c].BackColor != System.Drawing.Color.LimeGreen) grille[l, c].Enabled = false;
                            else grille[l, c].Enabled = true;
                        }
                    }
                    
                }
                richTextBox1.Text = richTextBox2.Text + " : " + score;
            }
        }

        private void button66_Click(object sender, EventArgs e)
        {
            button67.Enabled = false;
            solution = true;
            
            if(button66.Text == "Solution")
            {
                for (int l = 0; l < 8; l++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        grille[l, c].Enabled = true;
                        grille[l, c].Text = "";
                        if (l % 2 == 0)
                        {
                            if (c % 2 == 0) grille[l, c].BackColor = System.Drawing.Color.Beige;
                            else grille[l, c].BackColor = System.Drawing.Color.Chocolate;

                        }
                        else
                        {
                            if (c % 2 == 0) grille[l, c].BackColor = System.Drawing.Color.Chocolate;
                            else grille[l, c].BackColor = System.Drawing.Color.Beige;
                        }
                    }
                }
                MessageBox.Show("Hey! loser, une fois que t'as cliqué sur 'ok', choisi ta case de départ",
                            "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            richTextBox2.Enabled = true;
            if (richTextBox2.Text == "")
            {
                MessageBox.Show("Veillez entrer votre nom !!!",
                            "Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                button67.Enabled = true;
                richTextBox2.Enabled = false;
                go = false;
                solution = false;
                richTextBox1.Text = richTextBox2.Text + " : " + 0;
                score = 1;
                for (int l = 0; l < 8; l++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        grille[l, c].Enabled = true;
                        grille[l, c].Text = "";
                    }
                }
                button55.Text = "Jouer";
            }
        }
    }
}
