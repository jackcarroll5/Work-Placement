using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace XsAndOs
{
    class XAndOEngine
    {
        private int turn = 1;

        private int[,] winner = new int[,]
        {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };

        public bool checkWinner(Button[] buttons)
        {    
            bool gameOver = false;

            for(int i = 0; i < 8; i++)
            {
                int a = winner[i, 0], b = winner[i, 1], c = winner[i, 2];
                Button b1 = buttons[a], b2 = buttons[b], b3 = buttons[c];

                if (b1.Text == "" || b2.Text == "" || b3.Text == "")
                    continue;

                if(b1.Text == b2.Text && b2.Text == b3.Text)
                {
                    b1.BackgroundColor = b2.BackgroundColor = b3.BackgroundColor = Color.Aqua;
                    gameOver = true;
                    break;
                }
            }

            bool tie = true;
            if(!gameOver)
            {
                foreach(Button b in buttons)
                {
                    if(b.Text == "")
                    {
                        tie = false;
                    }   break;
                }
            }
            if(tie)
            {
                gameOver = true;
            }

            return gameOver;
        }

        public void SetButton(Button b)
        {
          if(b.Text == "")
            {
                b.Text = turn == 1 ? "X" : "O";
                turn = turn == 1 ? 2 : 1;
            }
        }

    public void gameReset(Button[] buttons)
        {
            turn = 1;
            foreach(Button b in buttons)
            {
                b.Text = "";
                b.BackgroundColor = Color.Gray;
            }
        }

    }  
}
