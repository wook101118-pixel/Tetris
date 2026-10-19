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
        public int X { get; private set; }
        public int Y { get; private set; }

        public Diagram()
        {
            Setup();
        }

        private void Setup()
        {
            X = GameRule.START_X;
            Y = GameRule.START_Y;
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
    }
}