using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    // 게임의 상태를 관리하는 클래스
    internal class Game
    {
        //private readonly static Game _instance = new Game();
        //public static Game Instance = _instance;
        private Diagram now;
        internal Point NowPosition
        {
            get
            {
                if (now == null)
                {
                    return new Point(0, 0);
                }
                else
                {
                    return new Point(now.X, now.Y);
                }
            }
        }

        public int BlockNum
        {
            get
            {
                return now.BlockNum;
            }
        }

        public int Turn
        {
            get
            {
                return now.Turn;
            }
        }

        internal static Game Singleton { get; private set; }

        // 정적 생성자: 클래스가 처음 사용될 때 한 번만 호출됨
        static Game()
        {
            Singleton = new Game();
        }

        // 일반 생성자: 외부에서 호출할 수 없음
        private Game()
        {
            now = new Diagram();
        }

        internal bool MoveLeft()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if (now.X + xx <= 0)
                        {
                            return false;
                        }
                    }
                }
            }

            now.MoveLeft();
            return true;
        }

        internal bool MoveRight()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if (now.X + xx + 1 >= GameRule.B_WIDTH)
                        {
                            return false;
                        }
                    }
                }
            }

            now.MoveRight();
            return true;
        }

        internal bool MoveDown()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if(now.Y + yy + 1 >= GameRule.B_HEIGHT)
                        {
                            return false;
                        }
                    }
                }
            }

            now.MoveDown();
            return true;
        }

        internal bool MoveTurn()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for(int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, (Turn + 1) % 4, xx, yy] != 0)
                    {
                        if (now.X + xx <= 0 || now.X + xx + 1 >= GameRule.B_WIDTH
                            || now.Y + yy + 1 >= GameRule.B_HEIGHT)
                        {
                            return false;
                        }
                    }
                }
            }

            now.MoveTurn();
            return true;
        }

        internal void Next()
        {
            now.Setup();
        }
    }
}