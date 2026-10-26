using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{ 
    // 도형 모양 정의
    // 도형 돌리기
    internal class Diagram
    {
        public int X         { get; private set; }
        public int Y         { get; private set; }

        public int BlockNum  { get; private set; }
        public int Turn      { get; private set; }

        public Diagram()
        {
            Setup();
        }

        internal void Setup()
        {
            Random rand  = new Random();
            X            = GameRule.START_X;
            Y            = GameRule.START_Y;
            BlockNum     = rand.Next() % 7;
            Turn         = rand.Next() % 4;  // 0,1,2,3
        }

        public void MoveLeft()
        {
            X--;
        }

        public void MoveRight()
        {
            X++;
        }

        public void MoveDown()
        {
            Y++;
        }

        public void MoveTurn()
        {
            Turn = (Turn + 1) % 4;
        }
    }
}