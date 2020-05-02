using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;


namespace Snake
{
    class Obstacle
    {
        private List<Position> obs;
        private List<int> x;
        private List<int> y;

        public void Generate_random_obstacle(int num)
        {
            Random random = new Random();
            obs = new List<Position>();
            x = new List<int>();
            y = new List<int>();

            for (int i = 1; i < num; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                int x = random.Next(Console.WindowHeight);
                int y = random.Next(Console.WindowWidth);
                Position pos = new Position(x,y);
                obs.Add(pos);
            }
        }

        public List<Position> GetObsPos
        {
            get { return obs; }
        }

        //public void ViewObs()
//       {
//            for (int i = 0; i < obs.Count; i++)
//            {
//                Console.WriteLine("obs Row:" + GetObsPos[i].row + " obs Col:" + GetObsPos[i].col);
//            }
        }

//       public void getObsRow()
//       {
//            for (int i = 0; i < obs.Count; i++)
//            {
//                x.Add(GetObsPos[i].row);
//            }
//        }

//        public void getObsCol()
//       {
//            for (int i = 0; i < obs.Count; i++)
//            {
//                y.Add(GetObsPos[i].col);
//           }
//       }

//        public List<int> GetObsRow
//        {
//            get { return x; }
//        }

//        public List<int> GetObsCol
//        {
//            get { return y; }
//        }
//    }
}
