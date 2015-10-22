using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Codebreaker
{
    class Code
    {
        private int num = 0;
        public Code(int code)
        {
            this.num = code;
        }
        public int guessRed(int guess)
        {
            String intern = num.ToString();
            String ext = guess.ToString();

            Char[] alpha = intern.ToCharArray();
            Char[] beta = ext.ToCharArray();

            int num_red = 0;

            for (int col = 0; col < alpha.Length; col++)
            {
                char testalpha = alpha[col];

                for (int row = 0; row < beta.Length; row++)
                {
                    char testbeta = beta[row];
                    if (row != col && testbeta == testalpha)
                    {
                        num_red = num_red + 1;
                    }
                }
            }

            return num_red;
        }
        public int guessGreen(int guess)
        {
            String intern = num.ToString();
            String ext = guess.ToString();

            Char[] alpha = intern.ToCharArray();
            Char[] beta = ext.ToCharArray();

            int num_green = 0;

            for (int col = 0; col < alpha.Length; col++)
            {
                char testalpha = alpha[col];

                for (int row = 0; row < beta.Length; row++)
                {
                    char testbeta = beta[row];
                    if (row == col && testbeta == testalpha)
                    {
                        num_green = num_green + 1;
                    }
                }
            }

            return num_green;
        }
        public bool isEliminated(int othercode)
        {
            if (guessRed(othercode) == 0 && guessGreen(othercode) == 0)
            {
                return true;
            }
            else
            {
                if (guessGreen(othercode) == 4)
                {
                    return true;
                }
                return false;
            }
        }
        public int getNum()
        {
            return this.num;
        }
    }
}
