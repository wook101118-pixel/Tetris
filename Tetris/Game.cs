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
        Board gboard = Board.GameBoard;

        internal int this[int x, int y]
        {
            get
            {
                return gboard[x, y];
            }
        }
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

            if (gboard.MoveEnable(now.BlockNum, Turn, now.X - 1, now.Y))
            {
                now.MoveLeft();
                return true;
            }

            return false;
        }

        internal bool MoveRight()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if (now.X + xx + 1 >= GameRule.B_WIDTH_COUNT)
                        {
                            return false;
                        }
                    }
                }
            }

            if (gboard.MoveEnable(now.BlockNum, Turn, now.X + 1, now.Y))
            {
                now.MoveRight();
                return true;
            }

            return false;
        }

        internal bool MoveDown()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if(now.Y + yy + 1 >= GameRule.B_HEIGHT_COUNT)
                        {
                            gboard.Store(now.BlockNum, Turn, now.X, now.Y);
                            return false;
                        }
                    }
                }
            }


            if (gboard.MoveEnable(now.BlockNum, Turn, now.X, now.Y + 1))
            {
                now.MoveDown();
                return true;
            }

            gboard.Store(now.BlockNum, Turn, now.X, now.Y);
            return false;
        }

        internal bool MoveTurn()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for(int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, (Turn + 1) % 4, xx, yy] != 0)
                    {
                        if (now.X + xx < 0 || (now.X + xx >= GameRule.B_WIDTH_COUNT) || now.Y + yy >= GameRule.B_HEIGHT_COUNT)
                        {
                            return false;
                        }
                    }
                }
            }

            if (gboard.MoveEnable(now.BlockNum, (Turn + 1) % 4, now.X, now.Y))
            {
                now.MoveTurn();
                return true;
            }

            return false;
        }

        internal void Next()
        {
            now.Setup();
        }
    }
}
