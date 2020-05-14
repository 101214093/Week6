using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class User
    {
        private string name;
        private int score;
        private double clearTime;
        public User(string username) {
            name = username;
            score = 0;
        }

        //increase the user score by 1
        public void ScoreIncrement(int up) {
            score += up;
        }

        public string getName
        {
            get { return name; }
            set { name = value; }
        }

        public int getScore {
            get { return score; }
            set { score = value; }
        }

        public double getTime
        {
            get { return clearTime; }
            set { clearTime = value; }
        }
    }
}
