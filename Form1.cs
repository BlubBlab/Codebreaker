using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Codebreaker
{
    public partial class Form1 : Form
    {
        List<Code> list = new List<Code>();
        int red = 0;
        int green = 0;
        //int roundcounter = 0;
        public Form1()
        {
            InitializeComponent();
            generateCode();

            //Code test = new Code(4567);
            //  int gx = test.guessGreen(4567);
            // int gx2 = test.guessRed(4567);
            //textBox2.Text = "we found green "+gx+" and  red:"+gx2+"";

        }
        public void updateCounters()
        {
            textBox2.Text = red.ToString();
            textBox3.Text = green.ToString();
        }
        public void generateCode()
        {
            int i = 1111;
            //  Regex regex = new Regex(@"");
            while (i < 9876)
            {

                Char[] alpha = i.ToString().ToCharArray();
                Char[] beta = i.ToString().ToCharArray();

                int num_red = 0;

                for (int col = 0; col < alpha.Length; col++)
                {
                    char alphachar = alpha[col];

                    for (int row = 0; row < beta.Length; row++)
                    {
                        char betachar = beta[row];
                        if ((row != col && betachar == alphachar) || beta[row] == '0')
                        {
                            num_red = num_red + 1;
                        }
                    }
                }
                if (num_red == 0)
                {
                    Code theta = new Code(i);
                    list.Add(theta);
                }
                i = i + 1;

            }
            //  textBox5.Text = list.Count.ToString();
        }
        public void startS()
        {
            String round_count_txt = textBox4.Text;
            int round_count = Convert.ToInt32(round_count_txt);
            round_count = round_count + 1;
            textBox4.Text = round_count.ToString();
            String code_string = textBox1.Text;
            int usedcode = Convert.ToInt32(code_string);
            //   textBox2.Text = usedcode.ToString();
            solve(red, green, usedcode);
        }
        public void solve(int red, int green, int usedcode)
        {
            bool isallequal = true;
            //textBox2.Text = "Number used : " + usedcode + " number of reds: " + red + " Number of greens: " + green + "";
            Code used_element = null;
            foreach (Code element in list)
            {
                if (element.getNum() == usedcode)
                {
                    used_element = element;
                }
            }
            if (used_element != null && list.Count > 1)
            {
                list.Remove(used_element);
            }
            if (list.Count == 1)
            {
                int answer = (list.ToArray())[0].getNum();
                textBox1.Text = answer.ToString();
                System.Windows.Forms.MessageBox.Show("The number must be: " + answer + "");
                return;
            }

            List<Code> removelist = new List<Code>();
            foreach (Code element in list)
            {
                bool remove = false;
                if (element.guessGreen(usedcode) != green)
                {
                    remove = true;
                }
                if (element.guessRed(usedcode) != red)
                {
                    remove = true;
                }
                if (remove == true)
                {
                    removelist.Add(element);
                }
            }
            foreach (Code element in removelist)
            {
                list.Remove(element);
            }
            if (list.Count == 1)
            {
                int answer = (list.ToArray())[0].getNum();
                textBox1.Text = answer.ToString();
                System.Windows.Forms.MessageBox.Show("The number must be: " + answer + "");
                return;

            }
            if (list.Count == 0)
            {
                // int answer = (list.ToArray())[0].getNum();
                System.Windows.Forms.MessageBox.Show("Something went wrong no answer");
                return;
            }
            // find best code to guess
            int bestchoice = (list.ToArray())[0].getNum();
            int bestscore = 0;
            foreach (Code element2 in list)
            {
                if (element2.isEliminated(bestchoice))
                {
                    bestscore = bestscore + 1;
                }
            }

            foreach (Code element in list)
            {
                int guess = element.getNum();
                int score = 0;
                if (score < bestscore)
                {
                    isallequal = false;
                }
                foreach (Code element2 in list)
                {
                    if (element2.isEliminated(guess))
                    {
                        score = score + 1;
                    }
                }
                if (score > bestscore)
                {
                    bestscore = score;
                    bestchoice = guess;
                    isallequal = false;
                }
            }
            if (bestscore <= 1 || (isallequal && list.Count > 1))
            {
                bestscore = 500;
                foreach (Code element in list)
                {
                    int guess = element.getNum();
                    int score = 0;
                    foreach (Code element2 in list)
                    {

                        score = score + (element2.guessGreen(guess) + element2.guessRed(guess));

                    }
                    if (score < bestscore && score > 0)
                    {
                        bestscore = score;
                        bestchoice = guess;
                    }
                }
            }
            textBox1.Text = bestchoice.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1357";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            red = 1;
            updateCounters();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            red = 2;
            updateCounters();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            red = 3;
            updateCounters();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            red = 4;
            updateCounters();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            green = 1;
            updateCounters();
            //    startS();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            green = 2;
            updateCounters();
            //  startS();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            green = 3;
            updateCounters();
            //  startS();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            list.RemoveRange(0, list.Count);
            generateCode();
            red = 0;
            green = 0;
            textBox4.Text = "0";
            textBox1.Text = "1357";
            updateCounters();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            startS();
            red = 0;
            green = 0;
            updateCounters();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            red = 0;
            updateCounters();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            green = 0;
            updateCounters();
        }
    }
}
